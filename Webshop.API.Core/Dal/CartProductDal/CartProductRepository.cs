using Microsoft.EntityFrameworkCore;
using Webshop.API.Core.Dal.Common;

namespace Webshop.API.Core.Dal.CartProductDal;

public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
{
    private readonly ApiContext _dbContext;

    public CartProductRepository(ApiContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public CartProduct? GetByUserAndEan(string user, string ean)
    {
        return _dbContext.CartProducts
            .Include(cp => cp.ShoppingCart)
            .FirstOrDefault(cp => cp.Ean == ean && cp.ShoppingCart.User == user);
    }
}