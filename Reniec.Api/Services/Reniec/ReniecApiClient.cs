using Microsoft.Extensions.Options;
using Reniec.Api.Models;
using ServiceReniecSoap;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Reniec.Api.Services
{
    public class ReniecApiClient : IReniecApiClient
    {
        private readonly HttpClient _apiClient;
        private readonly ServiceReniec serviceReniecConfig;
        //private readonly SRELServiceService _sRELServiceService;

        public ReniecApiClient(
            HttpClient httpClient,
            //SRELServiceService sRELServiceService,
            IOptions<ServiceReniec> serviceReniecOptions
            )
        {
            _apiClient = httpClient;
            //_logger = logger;
            serviceReniecConfig = serviceReniecOptions.Value;
            //_sRELServiceService = sRELServiceService;
        }

        /// <summary>
        /// Obtiene los datos de una persona desde el servicio de reniec
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        public async Task<resultadoConsulta> ObtenerPersonaReniec(string numeroDocumento, CancellationToken cancelationToken)
        {
            SRELServiceServiceClient sRELServiceService = new SRELServiceServiceClient();

            peticionConsulta _peticionConsulta = this.SetSettingService();
            _peticionConsulta.nuDniConsulta = numeroDocumento;

            await sRELServiceService.OpenAsync();
            var resp = await sRELServiceService.consultarAsync(_peticionConsulta);
            await sRELServiceService.CloseAsync();

            return resp;
        }

        private peticionConsulta SetSettingService()
        {
            return new peticionConsulta() { nuDniUsuario = serviceReniecConfig.nuDniUsuario, nuRucUsuario = serviceReniecConfig.nuRucUsuario, password = serviceReniecConfig.password };
        }
    }

    #region CLASES PARA MAPEO

    /// <summary>
    /// Asociada a la seccion ServicioReniec del archivo de configuracion
    /// </summary>
    public class ServiceReniec
    {
        public string nuDniUsuario { get; set; }
        public string nuRucUsuario { get; set; }
        public string password { get; set; }
    }
    #endregion
}
