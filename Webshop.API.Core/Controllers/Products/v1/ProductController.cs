using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Webshop.API.Core.Models;

namespace Webshop.API.Core.Controllers.Products.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductViewModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}