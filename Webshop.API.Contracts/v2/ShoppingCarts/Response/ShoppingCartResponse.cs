namespace Webshop.API.Contracts.v2.ShoppingCarts.Response;

public class ShoppingCartResponse
{
    public IEnumerable<CartProductResponse> CardProducts { get; set; }
    public string User { get; set; }
    public string Summary { get; set; }
    public decimal Total { get; set; }
}