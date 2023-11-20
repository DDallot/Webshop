using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ShoppingCartDal;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApiContext _dbContext;

    public ShoppingCartRepository(ApiContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ShoppingCart? GetByUser(string user)
    {
        return _dbContext.ShoppingCarts.FirstOrDefault(s => s.User == user);
    }
}