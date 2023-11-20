using System.ComponentModel.DataAnnotations;
using Webshop.API.Dal.Common;
using Webshop.API.Dal.ShoppingCartDal;

namespace Webshop.API.Dal.CartProductDal;

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