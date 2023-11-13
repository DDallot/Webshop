using System.ComponentModel.DataAnnotations;
using Webshop.API.Core.Dal.Common;
using Webshop.API.Core.Dal.ShoppingCartDal;

namespace Webshop.API.Core.Dal.CartProductDal;

public class CartProduct : EntityBase
{
    [Required]
    public string Ean { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Currency { get; set; }

   
    public ShoppingCart ShoppingCart { get; set; }
}