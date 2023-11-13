using Webshop.API.Core.Dal.Common;

namespace Webshop.API.Core.Dal.ShoppingCartDal;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    ShoppingCart? GetByUser(string user);
}
