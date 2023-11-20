using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ShoppingCartDal;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    ShoppingCart? GetByUser(string user);
}
