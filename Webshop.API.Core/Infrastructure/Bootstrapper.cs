using Webshop.API.Dal.CartProductDal;
using Webshop.API.Dal.ProductDal;
using Webshop.API.Dal.ShoppingCartDal;
using Webshop.API.Core.Services.DiscountsService.v2;
using Webshop.API.Core.Services.ProductsService.v2;
using Webshop.API.Core.Services.ShoppingCartsService.v2;
using Webshop.API.Core.Services.DiscountsService.v2.DiscountRules;

namespace Webshop.API.Core.Infrastructure;

public static class Bootstrapper
{
    public static IServiceProvider Initialize(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Services
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddScoped<IShoppingCartService, ShoppingCartService>();
        serviceCollection.AddSingleton<IDiscountService, DiscountService>();

        // Rules
        serviceCollection.AddSingleton<IDisccountRule, MoreThan2HeinekensDiscountRule>();
        serviceCollection.AddSingleton<IDisccountRule, MoreThan2BudLightsDiscountRule>();
        serviceCollection.AddSingleton<IDisccountRule, UiltjeIPAMikkelerIPADiscountRule>();

        // Dal
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ICartProductRepository, CartProductRepository>();
        serviceCollection.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

        return serviceCollection.BuildServiceProvider();
    }
}