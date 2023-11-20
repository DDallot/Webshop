using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.CartProductDal;

public interface ICartProductRepository : IRepository<CartProduct>
{
    CartProduct? GetByUserAndEan(string user, string ean);
}