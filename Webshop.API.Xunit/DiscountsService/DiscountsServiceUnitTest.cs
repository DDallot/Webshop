using Xunit;
using Webshop.API.Core.Services.DiscountRules;
using Webshop.API.Core.Services.DiscountsService.v2;
using Webshop.API.Core.Dal.CartProductDal;
using FluentAssertions;
using Webshop.API.Core.Dal.ProductDal;

namespace Webshop.API.Xunit.DiscountsService;

public class DiscountsServiceUnitTest
{
    private readonly IDiscountService _discountService;
    private readonly Product Heineken = new() { Ean = "00001111", Price = 1.99m };
    private readonly Product Uiltje   = new() { Ean = "00002222", Price = 2.49m };
    private readonly Product BudLight = new() { Ean = "00003333", Price = 0.99m };
    private readonly Product Mikkeler = new() { Ean = "00004444", Price = 16.99m};

    public DiscountsServiceUnitTest() 
    {
        var rules = new List<IDisccountRule>
        {
            new MoreThan2BudLightsDiscountRule(),
            new MoreThan2HeinekensDiscountRule(),
            new UiltjeIPAMikkelerIPADiscountRule()
        };
        _discountService = new DiscountService(rules);
    }

    [Fact] //cart 1: [2x "Heineken Lager", 1x "Mikkeler IPA"] -> $20.97
    public async Task CalculateDiscount_NoDiscount()
    {
        var value = new List<CartProduct>
        {
            new CartProduct{Quantity = 2, Ean = Heineken.Ean, Price = Heineken.Price},
            new CartProduct{Quantity = 1, Ean = Mikkeler.Ean, Price = Mikkeler.Price}
        };
        var expected = 20.97m;

        var result = await _discountService.CalculateDiscountAsync(value);

        result.Total.Should().Be(expected);
    }

    [Fact] //cart 2: [5x "Heineken Lager", 1x "Mikkeler IPA", 1x "Bud Light"] -> $23.95
    public async Task CalculateDiscount_HeinkenDiscount()
    {
        var value = new List<CartProduct>
        {
            new CartProduct{Quantity = 5, Ean = Heineken.Ean, Price = Heineken.Price},
            new CartProduct{Quantity = 1, Ean = Mikkeler.Ean, Price = Mikkeler.Price},
            new CartProduct{Quantity = 1, Ean = BudLight.Ean, Price = BudLight.Price}
        };
        var expected = 23.95m;

        var result = await _discountService.CalculateDiscountAsync(value);

        result.Total.Should().Be(expected);
    }

    [Fact] //cart 3: [2x "Uiltje IPA", 8x "Bud Light"] -> $11.316
    public async Task CalculateDiscount_BudLightDiscount()
    {
        var value = new List<CartProduct>
        {
            new CartProduct{Quantity = 2, Ean = Uiltje.Ean, Price = Uiltje.Price},
            new CartProduct{Quantity = 8, Ean = BudLight.Ean, Price = BudLight.Price}
        };
        var expected = 11.316m;

        var result = await _discountService.CalculateDiscountAsync(value);

        result.Total.Should().Be(expected);
    }

    [Fact] //cart 4: [1x "Heineken Lager", 1x "Bud Light", 1x "Uiltje IPA", 1x "Mikkeler IPA"] -> $17.46
    public async Task CalculateDiscount_UiltjeAndMikkelerDiscount()
    {
        var value = new List<CartProduct>
        {
            new CartProduct{Quantity = 1, Ean = Heineken.Ean, Price = Heineken.Price},
            new CartProduct{Quantity = 1, Ean = BudLight.Ean, Price = BudLight.Price},
            new CartProduct{Quantity = 1, Ean = Uiltje.Ean, Price = Uiltje.Price},
            new CartProduct{Quantity = 1, Ean = Mikkeler.Ean, Price = Mikkeler.Price},
        };
        var expected = 17.46m;

        var result = await _discountService.CalculateDiscountAsync(value);

        result.Total.Should().Be(expected);
    }

    [Fact] //cart 5: [3x "Heineken Lager", 2x "Uiltje IPA", 1x "Mikkeler IPA"] -> $20.95
    public async Task CalculateDiscount_TwoDiscounts()
    {
        var value = new List<CartProduct>
        {
            new CartProduct{Quantity = 3, Ean = Heineken.Ean, Price = Heineken.Price},
            new CartProduct{Quantity = 2, Ean = Uiltje.Ean, Price = Uiltje.Price},
            new CartProduct{Quantity = 1, Ean = Mikkeler.Ean, Price = Mikkeler.Price},
        };
        var expected = 20.95m;

        var result = await _discountService.CalculateDiscountAsync(value);

        result.Total.Should().Be(expected);
    }
}