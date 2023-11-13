using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.Products.Response;
using Webshop.API.Core.Services.ProductsService.v2;

namespace Webshop.API.Core.Controllers.Products.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Get a list of all products 
        /// </summary>
        /// <returns>Return Products</returns>
        [HttpGet]
        public async Task<ListResult<ProductResponse>> GetProducts()
        {
            return await _productService.GetProductsAsync();
        }

        /// <summary>
        /// Get 1 specific product
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>Return a product</returns>
        [HttpGet("{identifier}")]
        public async Task<ItemResult<ProductResponse>> GetProductAsync([FromRoute] int identifier)
        {
            return await _productService.GetProductAsync(identifier);
        }
    }
}