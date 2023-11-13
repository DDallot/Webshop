using Webshop.API.Core.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountsService.v2;

public interface IDiscountService
{
    Task<DiscountInfo> CalculateDiscountAsync(List<CartProduct> cartProducts);
}
