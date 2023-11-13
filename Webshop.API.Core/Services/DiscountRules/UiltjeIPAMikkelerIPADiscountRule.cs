using Webshop.API.Core.Dal.CartProductDal;

namespace Webshop.API.Core.Services.DiscountRules
{
    public class UiltjeIPAMikkelerIPADiscountRule : IDisccountRule
    {
        private const string UiltjeIPA = "00002222";
        private const string MikkelerIPA = "00004444";
        private const string Summary = "{0} Uiltje IPA and {1} Mikkeler IPA, you got $5 off";

        // If a customer gets an Uiltje IPA and a Mikkeler IPA, they get $5 off the order total
        public DiscountRuleInfo CalculateDiscountAmount(Dictionary<string, CartProduct> dic)
        {
            if (!dic.TryGetValue(UiltjeIPA, out var productUiltje)) return new DiscountRuleInfo();
            if (!dic.TryGetValue(MikkelerIPA, out var productMikkeler)) return new DiscountRuleInfo();

            return new DiscountRuleInfo
            {
                DiscountAmount = 5,
                Summary = string.Format(Summary, productUiltje.Quantity, productMikkeler.Quantity)
            };
        }
    }
}
