namespace Webshop.API.Contracts.v2.ShoppingCarts.Response;

public class CartProductResponse
{
    public string Ean { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}