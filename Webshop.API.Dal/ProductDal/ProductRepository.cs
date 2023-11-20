using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ProductDal;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApiContext _dbContext;

    public ProductRepository(ApiContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public IQueryable<Product> GetProducts()
    {
        return _dbContext.Products;
    }
}