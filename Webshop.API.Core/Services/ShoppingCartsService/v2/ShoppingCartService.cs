using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.ShoppingCarts.Response;
using Webshop.API.Dal.CartProductDal;
using Webshop.API.Dal.ProductDal;
using Webshop.API.Dal.ShoppingCartDal;
using Webshop.API.Core.Services.DiscountsService.v2;
using Webshop.API.Core.Services.ProductsService.v2;

namespace Webshop.API.Core.Services.ShoppingCartsService.v2;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ICartProductRepository _cartProductRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;
    private readonly ILogger<ShoppingCartService> _logger;

    public ShoppingCartService(
        IShoppingCartRepository shoppingCartRepository,
        ICartProductRepository cartProductRepository,
        IProductRepository productRepository,
        IDiscountService discountService,
        IMapper mapper,
        ILogger<ShoppingCartService> logger)
    {
        _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        _cartProductRepository = cartProductRepository ?? throw new ArgumentNullException(nameof(cartProductRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ItemResult<bool>> AddProductAsync(string user, int productId)
    {
        try {
            var shoppingCart = await _shoppingCartRepository
                .List()
                .FirstOrDefaultAsync(s => s.User == user) ?? new ShoppingCart { User = user};

            var product = _productRepository.GetById(productId);

            if(product == null) 
                return new ItemResult<bool> { HasError = true, Error = "Product not found."};
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
            _logger.LogError("Error on service {0}, method {1}, exeception {2}", nameof(ShoppingCartService), nameof(AddProductAsync), ex.Message);
            return new ItemResult<bool>
            {
                HasError = true,
                Error = "Error adding a Product."
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
            _logger.LogError("Error on service {0}, method {1}, exeception {2}", nameof(ShoppingCartService), nameof(GetSummaryAsync), ex.Message);

            return new ItemResult<ShoppingCartResponse>
            {
                HasError = true,
                Error = "Error getting the Summary."
            };
        }
    }
}