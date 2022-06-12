using Microsoft.AspNetCore.Mvc;
using OpenTelemetryExample.Core.Clients;
using OpenTelemetryExample.Core.IoC;
using OpenTelemetryExample.Core.Models;

namespace OpenTelemetryExample.Api2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SenderController : ControllerBase
    {
        private readonly IApi1Client _client = ContainerManager.Resolve<IApi1Client>();
        private readonly ILogger<SenderController> _logger;

        public SenderController(ILogger<SenderController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        public async Task<bool> SendToApi1(ProductDto product)
        {
            try
            {
                return await _client.PostProductAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}