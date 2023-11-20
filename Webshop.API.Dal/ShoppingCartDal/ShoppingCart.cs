using Webshop.API.Dal.CartProductDal;
using Webshop.API.Dal.Common;

namespace Webshop.API.Dal.ShoppingCartDal;

public class ShoppingCart : EntityBase
{
    public string User { get; set; }

    public List<CartProduct> CartProducts { get; set; }
}