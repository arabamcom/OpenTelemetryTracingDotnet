using Microsoft.Extensions.Logging;
using OpenTelemetryExample.Core.IoC;
using OpenTelemetryExample.Core.Models;
using OpenTelemetryExample.Core.Repositories.Product;

namespace OpenTelemetryExample.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository = ContainerManager.Resolve<IProductRepository>();
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            try
            {
                return await _productRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ürün çekilirken hata meydana geldi! Ex: {0} Id: {1}", ex, id);
                throw;
            }
        }

        public async Task InsertProductAsync(ProductDto productDto)
        {
            try
            {
                await _productRepository.AddAsync(new Product()
                {
                    Name = productDto.Name,
                    Barcode = productDto.Barcode,
                    Description = productDto.Description,
                    Rate = productDto.Rate,
                    AddedOn = DateTime.Now,
                    ModifiedOn = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Ürün eklenirken hata meydana geldi! Ex: {0} ProductDto: {1}", ex, productDto);
                throw;
            }
        }
    }
}
