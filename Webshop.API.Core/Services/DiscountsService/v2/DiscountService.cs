using System.Text;
using Webshop.API.Dal.CartProductDal;
using Webshop.API.Core.Services.DiscountsService.v2.DiscountRules;

namespace Webshop.API.Core.Services.DiscountsService.v2;

public class DiscountService : IDiscountService
{
    private readonly IEnumerable<IDisccountRule> _rules;

    public DiscountService(IEnumerable<IDisccountRule> rules) 
    {
        _rules = rules ?? throw new ArgumentNullException(nameof(rules));
    }

    public async Task<DiscountInfo> CalculateDiscountAsync(List<CartProduct> cartProducts)
    {
        decimal subTotal = cartProducts.Sum(cp => cp.Quantity * cp.Price);
        decimal total = subTotal;

        var summaryDiscount = new StringBuilder();
        var dic = cartProducts.ToDictionary(cp => cp.Ean);
        foreach (var rule in _rules)
        {
            var discount = rule.CalculateDiscountAmount(dic);
            if (discount.DiscountAmount == 0) continue;
            total -= discount.DiscountAmount;
            summaryDiscount.AppendLine($"Discount: {discount.DiscountAmount} - {discount.Summary}");
        }

        return  await Task.FromResult(new DiscountInfo
        {
            SubTotal = subTotal,
            Total = total,
            SummaryDiscount = summaryDiscount.ToString()
        });
    }
}