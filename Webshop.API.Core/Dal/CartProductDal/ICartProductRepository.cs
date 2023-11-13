using Webshop.API.Core.Dal.Common;

namespace Webshop.API.Core.Dal.CartProductDal
{
    public interface ICartProductRepository : IRepository<CartProduct>
    {
        CartProduct? GetByUserAndEan(string user, string ean);
    }
}
