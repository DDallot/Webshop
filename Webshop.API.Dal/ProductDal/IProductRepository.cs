using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ProductDal;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> GetProducts();
}
