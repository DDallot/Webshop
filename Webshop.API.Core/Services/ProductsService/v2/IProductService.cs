using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.Products.Response;

namespace Webshop.API.Core.Services.ProductsService.v2;

public interface IProductService
{
    Task<ItemResult<ProductResponse>> GetProductAsync(int identifier);
    Task<ListResult<ProductResponse>> GetProductsAsync();
}