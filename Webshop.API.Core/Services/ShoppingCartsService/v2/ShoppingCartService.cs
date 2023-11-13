using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.ShoppingCarts.Response;
using Webshop.API.Core.Dal.CartProductDal;
using Webshop.API.Core.Dal.ProductDal;
using Webshop.API.Core.Dal.ShoppingCartDal;
using Webshop.API.Core.Services.DiscountsService.v2;

namespace Webshop.API.Core.Services.ShoppingCartsService.v2;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public ShoppingCartService(
        IShoppingCartRepository shoppingCartRepository,
        ICartProductRepository cartProductRepository,
        IProductRepository productRepository,
        IDiscountService discountService,
        IMapper mapper)
    {
        _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _cartProductRepository = cartProductRepository ?? throw new ArgumentNullException(nameof(cartProductRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ItemResult<bool>> AddProductAsync(string user, int productId)
    {
        try {
            var shoppingCart = await _shoppingCartRepository
                .List()
                .FirstOrDefaultAsync(s => s.User == user) ?? new ShoppingCart { User = user};

            var product = _productRepository.GetById(productId);

            if(product == null) 
                return new ItemResult<bool> { HasError = true, Errors = new List<string> { "Product not found." } };
            if (product.Quantity < 1) 
                return new ItemResult<bool> { Item = false };

            product.Quantity -= 1;
            _productRepository.Edit(product);

            var cardProduct = _cartProductRepository.GetByUserAndEan(user, product.Ean);

            if (cardProduct == null)
            {
                cardProduct = new CartProduct
                {
                    Ean = product.Ean,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Currency = product.Currency,
                    ShoppingCart = shoppingCart
                };
                _cartProductRepository.Add(cardProduct);
            }
            else
            {
                cardProduct.Quantity += 1;
                _cartProductRepository.Edit(cardProduct);
            }

            await _cartProductRepository.SaveAsync(cardProduct);

            return await Task.FromResult(new ItemResult<bool> { Item = true });
        } catch (Exception ex)
        {
            return new ItemResult<bool>
            {
                HasError = true,
                Errors = new List<string>()
                {
                    $"Error on service {nameof(ShoppingCartService)}, method {nameof(AddProductAsync)}",
                    ex.Message
                }
            };
        }
    }

    public async Task<ItemResult<ShoppingCartResponse>> GetSummaryAsync(string user)
    {
        try
        {
            var shoppingCart = await _shoppingCartRepository
                .List()
                .Include(sc => sc.CartProducts)
                .FirstOrDefaultAsync(s => s.User == user);

            if (shoppingCart == null)
            {
                return new ItemResult<ShoppingCartResponse>
                {
                   Item = new ShoppingCartResponse
                   {
                       Summary = "Shopping Cart is empty"
                   }
                };
            }

            var cartProducts = shoppingCart.CartProducts ?? new List<CartProduct> ();
            var summary = new StringBuilder("Shopping Cart");
            summary.AppendLine();
            cartProducts.ForEach(cp => summary.AppendLine($"{cp.Quantity} {cp.Name} - {cp.Quantity * cp.Price}"));

            var discountInfo = await _discountService.CalculateDiscountAsync(cartProducts);

            if(discountInfo.SubTotal == discountInfo.Total)
            {
                summary.AppendLine($"Total: {discountInfo.Total}");
            }
            else
            {
                summary.AppendLine($"Sub Total: {discountInfo.SubTotal}");
                summary.AppendLine(discountInfo.SummaryDiscount);
                summary.AppendLine($"Total: {discountInfo.Total}");
            }

            var cartProductsResponse = _mapper.Map<List<CartProductResponse>>(cartProducts);

            return new ItemResult<ShoppingCartResponse>
            {
                Item = new ShoppingCartResponse
                {
                    User = user,
                    Summary = summary.ToString(),
                    CardProducts = cartProductsResponse,
                    Total = discountInfo.Total
                }
            };
        }
        catch (Exception ex)
        {
            return new ItemResult<ShoppingCartResponse>
            {
                HasError = true,
                Errors = new List<string>()
                {
                    $"Error on service {nameof(ShoppingCartService)}, method {nameof(GetSummaryAsync)}",
                    ex.Message
                }
            };
        }
    }
}