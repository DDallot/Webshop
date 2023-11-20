using Webshop.API.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountsService.v2.DiscountRules;

public class MoreThan2HeinekensDiscountRule : IDisccountRule
{
    private const string HeinekenEan = "00001111";
    private const string Summary = "{0} Heineken Lagers, you got {1} free";

    // If a customer gets more than 2 Heineken Lagers, they get 1 free for each 2 in the cart
    public DiscountRuleInfo CalculateDiscountAmount(Dictionary<string, CartProduct> dic)
    {
        if (!dic.TryGetValue(HeinekenEan, out var product) || product.Quantity < 3) return new DiscountRuleInfo();

        int free = product.Quantity / 2;
        var amount = free * product.Price;

        return new DiscountRuleInfo
        {
            DiscountAmount = amount,
            Summary = string.Format(Summary, product.Quantity, free)
        };
    }
}