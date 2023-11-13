using Webshop.API.Core.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountRules;

public interface IDisccountRule
{
    DiscountRuleInfo CalculateDiscountAmount(Dictionary<string, CartProduct> dic);
}