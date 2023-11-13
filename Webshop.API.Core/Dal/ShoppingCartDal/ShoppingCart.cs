using Webshop.API.Core.Dal.CartProductDal;
using Webshop.API.Core.Dal.Common;

namespace Webshop.API.Core.Dal.ShoppingCartDal;

public class ShoppingCart : EntityBase
{
    public string User { get; set; }

    public List<CartProduct> CartProducts { get; set; }
}