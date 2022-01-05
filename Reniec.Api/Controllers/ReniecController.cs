using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reniec.Api.Services;
using ServiceReniecSoap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reniec.Api.Controllers
{
    [ApiController]
    [Route("api/v1/reniec")]
    public class ReniecController : ControllerBase
    {
        private readonly ILogger<ReniecController> _logger;
        private readonly IReniecApiClient _reniecApiClient;

        public ReniecController(ILogger<ReniecController> logger, IReniecApiClient reniecApiClient)
        {
            _logger = logger;
            _reniecApiClient = reniecApiClient;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> hi()
        {
            return Ok(new { Hi="Hi" });
        }

        [HttpGet]
        [Route("consultaPersona/{numeroDocumento}")]
        public async Task<IActionResult> consultaPersona([FromRoute] string numeroDocumento)
        {
            return Ok(await _reniecApiClient.ObtenerPersonaReniec(numeroDocumento, new CancellationToken()));
        }
    }
}
