using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Reniec.Api.Infraestructure.Filters
{
    public class RecaptchaFilter : IAsyncActionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public RecaptchaFilter(
            IWebHostEnvironment env,
            ILogger<HttpGlobalExceptionFilter> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory)
        {
            this.env = env;
            this.logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var client = _clientFactory.CreateClient();

            var recaptchaResponse = context.HttpContext.Request.Query["recaptchaResponse"][0];
            try
            {
                var parameters = new Dictionary<string, string>{
                    {"secret", this._configuration["Recaptcha:SecretKey"]},
                    {"response", recaptchaResponse},
                    {"remoteip", context.HttpContext.Connection.RemoteIpAddress.ToString()}
                };

                HttpResponseMessage response = await client.PostAsync(
                    this._configuration["Recaptcha:VerifyUrl"],
                    new FormUrlEncodedContent(parameters)
                );
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();
                dynamic apiJson = JObject.Parse(apiResponse);
                if (apiJson.success != true)
                {
                    context.Result = new BadRequestObjectResult("There was an unexpected problem processing this request. Please try again.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Something went wrong with the API. Let the request through.
                context.Result = new BadRequestObjectResult("Unexpected error calling reCAPTCHA api.");
            }

            var result = await next();
        }

        //public override void OnResultExecuting(ResultExecutingContext context)
        //{

        //}

        //public void OnException(ExceptionContext context)
        //{
        //    logger.LogError(new EventId(context.Exception.HResult),
        //        context.Exception,
        //        context.Exception.Message);


        //    var json = new JsonErrorResponse
        //    {
        //        Messages = new[] { $@"{env.ApplicationName}: Ocurrió un error. Inténtalo de nuevo." }
        //    };

        //    if (env.IsDevelopment())
        //    {
        //        json.DeveloperMessage = context.Exception;
        //    }       
        //    context.Result = new InternalServerErrorObjectResult(json);
        //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        //    context.ExceptionHandled = true;
        //}

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }

    }
}
