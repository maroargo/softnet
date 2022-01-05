using Reniec.Api.Models;
using ServiceReniecSoap;
using System.Threading;
using System.Threading.Tasks;

namespace Reniec.Api.Services
{
    public interface IReniecApiClient
    {
        Task<resultadoConsulta> ObtenerPersonaReniec(string numeroDocumento, CancellationToken cancelationToken);
    }
}
