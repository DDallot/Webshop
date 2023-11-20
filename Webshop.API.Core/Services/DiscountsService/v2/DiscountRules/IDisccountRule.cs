using Webshop.API.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountsService.v2.DiscountRules;

public interface IDisccountRule
{
    DiscountRuleInfo CalculateDiscountAmount(Dictionary<string, CartProduct> dic);
}