using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.Products.Response;

namespace Webshop.API.Contracts.v2.Products;

public interface IProduct
{
    Task<ItemResult<ProductResponse>> GetProductAsync(int identifier);
    Task<ListResult<ProductResponse>> GetProducts();
}