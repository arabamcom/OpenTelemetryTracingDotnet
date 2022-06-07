using Microsoft.AspNetCore.Mvc;
using OpenTelemetryExample.Core.IoC;
using OpenTelemetryExample.Core.Kafka;
using OpenTelemetryExample.Core.Kafka.Events;
using OpenTelemetryExample.Core.Models;
using OpenTelemetryExample.Core.Services;

namespace OpenTelemetryExample.Api1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IKafkaProducerService _producer = ContainerManager.Resolve<IKafkaProducerService>();
        private readonly IProductService _productService = ContainerManager.Resolve<IProductService>();
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IOperationResult> SendToKafkaAsync(ProductDto product)
        {
            try
            {
                var result = await _producer.ProduceAsync(product, IEventTopicType.AddWeatherForecastV1.ToString());
                if(result.Success)
                {
                    await _productService.InsertProductAsync(product);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}