using AutoMapper;
using Webshop.API.Contracts.v2.Products.Response;
using Webshop.API.Contracts.v2.ShoppingCarts.Response;
using Webshop.API.Dal.CartProductDal;
using Webshop.API.Dal.ProductDal;

namespace Webshop.API.Core.Services.Mapping;

public class WebshopProfiles : Profile
{
    public WebshopProfiles()
    {
        CreateMap<Product, ProductResponse>();

        CreateMap<CartProduct, CartProductResponse>();
    }
}
