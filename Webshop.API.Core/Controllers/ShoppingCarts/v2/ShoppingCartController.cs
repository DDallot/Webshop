using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.ShoppingCarts.Response;
using Webshop.API.Core.Services.ShoppingCartsService.v2;

namespace Webshop.API.Core.Controllers.ShoppingCarts.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService ?? throw new ArgumentNullException(nameof(shoppingCartService));
    }

    /// <summary>
    /// Place a product in a shopping cart
    /// </summary>
    /// <param name="user">User name</param>
    /// <param name="productId">Product to be added on the shopping cart</param>
    /// <returns>Return operation status</returns>
    [HttpPost("{user}/AddProduct")]
    public async Task<ItemResult<bool>> AddProductAsync([FromRoute] string user, [FromBody] int productId)
    {
        return await _shoppingCartService.AddProductAsync(user, productId);
    }

    /// <summary>
    /// Get the shopping cart summary
    /// </summary>
    /// <param name="user">User name</param>
    /// <returns>Return summary object</returns>
    [HttpGet("{user}/summary")]
    public async Task<ItemResult<ShoppingCartResponse>> GetSummaryAsync([FromRoute]string user)
    {
        return await _shoppingCartService.GetSummaryAsync(user);
    }
}