using EasyShop.DTO;
using EasyShop.Persistence.Models;
using EasyShop.Services.DTO;
using EasyShop.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyShop.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productRepository;
        private readonly IMemoryCache _cache;

        public ProductController(IProductService productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }

        [HttpGet("{SEOFriendlyName}")]
        public async Task<ActionResult<Product>> GetProductBySearchEngineFriendlyName(string SEOFriendlyName)
        {
            Product product;

            if (!_cache.TryGetValue(SEOFriendlyName, out product))
            {
                product = await _productRepository.GetProductByName(SEOFriendlyName);
                if (product == null)
                {
                    return NotFound();
                }
                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                // Save data in cache
                _cache.Set(SEOFriendlyName, product, cacheEntryOptions);
            }

            return product;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetProducts(
             [FromQuery] bool? inStock,
             [FromQuery] string variantColor,
             [FromQuery] string warehouseName,
             [FromQuery] string productName,
             [FromQuery] string variantSize,
             [FromQuery] int page = 1,
             [FromQuery] int pageSize = 10,
             [FromQuery] string sortBy = "CreatedOn",
             [FromQuery] bool sortAscending = false)
        {
            var filter = new FilterDto
            {
                InStock = inStock,
                VariantColor = variantColor,
                VariantSize = variantSize,
                WarehouseName = warehouseName,
                ProductName = productName,
            };

            var pagination = new PaginationDto
            {
                Page = page,
                PageSize = pageSize
            };

            var products = await _productRepository.GetAllProduct(filter, pagination, sortBy, sortAscending);
            return Ok(products);
        }
    }
}
