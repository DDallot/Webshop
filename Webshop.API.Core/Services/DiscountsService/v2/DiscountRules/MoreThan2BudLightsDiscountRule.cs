using Webshop.API.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountsService.v2.DiscountRules;

public class MoreThan2BudLightsDiscountRule : IDisccountRule
{
    private const string BudLight = "00003333";
    private const string Summary = "{0} Bud Lights, you got 20% off";

    // If a customer gets more than 2 Bud Lights, they get 20% off on all Bud Light beers in the cart
    public DiscountRuleInfo CalculateDiscountAmount(Dictionary<string, CartProduct> dic)
    {
        if (!dic.TryGetValue(BudLight, out var product) || product.Quantity < 3) return new DiscountRuleInfo();

        var amount = product.Quantity * product.Price * 0.2m;

        return new DiscountRuleInfo
        {
            DiscountAmount = amount,
            Summary = string.Format(Summary, product.Quantity)
        };
    }
}