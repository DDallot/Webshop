using Webshop.API.Core.Dal.Common;

namespace Webshop.API.Core.Dal.ProductDal
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProducts();
    }
}
