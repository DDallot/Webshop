namespace Webshop.API.Contracts.v2.Products.Response;

public class ProductResponse
{
    public int Identifier { get; set; }
    public string Ean { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
}