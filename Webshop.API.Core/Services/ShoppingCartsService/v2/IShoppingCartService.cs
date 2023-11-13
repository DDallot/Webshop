using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.ShoppingCarts.Response;

namespace Webshop.API.Core.Services.ShoppingCartsService.v2
{
    public interface IShoppingCartService
    {
        Task<ItemResult<bool>> AddProductAsync(string user, int productId);
        Task<ItemResult<ShoppingCartResponse>> GetSummaryAsync(string user);
    }
}
