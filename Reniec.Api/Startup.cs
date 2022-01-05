using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Reniec.Api.Infraestructure.Filters;
using Reniec.Api.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Reniec.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                .AddApplicationServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            var CorsOriginAllowed = configuration.GetSection("AllowedOrigins").Get<List<string>>();
            ///TODO: Si no se registra un origen en [AllowedOrigins] se asigna [*] para responder a cualquier origen por defecto
            var origins = CorsOriginAllowed != null ? CorsOriginAllowed.ToArray() : new string[] { "*" };

            Log.Information("Configurando Origenes para ({CORS})...", origins);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    //.SetIsOriginAllowed((host) => IsOriginAllowed(host, origins))
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.AllowCredentials()
                    );
            });
            Log.Information("Fin de Configuración ({CORS})...");

            services.AddOptions();
            services.Configure<ServiceReniec>(configuration.GetSection("ServicioReniec"));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));

            })
                        .AddNewtonsoftJson()
                        .AddControllersAsServices()
                        .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //register delegating handlers
            //services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            //services.AddTransient<HttpClientReniecDelegatingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<RecaptchaFilter>();

            //register http services

            services.AddHttpClient<IReniecApiClient, ReniecApiClient>((provider, config) =>
            {
                var serviceReniecConfig = provider.GetService<IOptions<ServiceReniec>>()?.Value;
                //config.BaseAddress = new Uri(serviceReniecConfig.Url);
                config.Timeout = TimeSpan.FromSeconds(300);
            });
                //.AddHttpMessageHandler<HttpClientReniecDelegatingHandler>();

           

            return services;
        }
        public static IServiceCollection AddCustomOcelot(this IServiceCollection services, IConfiguration configuration)
        {
            //Added by Antonio Diaz on March 2021 
            //Default
            //services.AddOcelot(configuration);

            //Ocelot & Cache
            //services.AddOcelot(configuration)
            //        .AddCacheManager(x =>
            //        {
            //            x.WithDictionaryHandle();
            //        });
            ////StackExchangeRedis
            //var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
            //services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

            ////Redis Cache para Ocelot
            //services.AddSingleton<IOcelotCache<CachedResponse>, InRedisCache<CachedResponse>>();

            return services;
        }
        internal static IAsyncPolicy<HttpResponseMessage> GetNoOpPolicy()
        {
            return Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();
        }
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
        private static bool IsOriginAllowed(string host, string[] corsOriginAllowed)
        {
            var iscors = corsOriginAllowed.Any(origin =>
               Regex.IsMatch(host, $@"^http(s)?://.*{origin}(:[0-9]+)?$", RegexOptions.IgnoreCase));

            return iscors;

        }
    }
}
