using AutoMapper;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.Products.Response;
using Webshop.API.Dal.ProductDal;

namespace Webshop.API.Core.Services.ProductsService.v2;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<ListResult<ProductResponse>> GetProductsAsync()
    {
        try
        {
            var products = _productRepository.List();

            var result = _mapper.Map<List<ProductResponse>>(products);

            return await Task.FromResult(new ListResult<ProductResponse> { Items = result });

        }catch(Exception ex)
        {
            _logger.LogError("Error on service {0}, method {1}, exeception {2}", nameof(ProductService), nameof(GetProductsAsync), ex.Message);
            return new ListResult<ProductResponse>
            {
                HasError = true,
                Error = "Error getting the Products."
            };
        }
    }

    public async Task<ItemResult<ProductResponse>> GetProductAsync(int identifier)
    {
        try
        {
            var products = _productRepository.GetById(identifier);

            var result = _mapper.Map<ProductResponse>(products);

            return await Task.FromResult(new ItemResult<ProductResponse> { Item = result });

        }
        catch (Exception ex)
        {
            _logger.LogError("Error on service {0}, method {1}, exeception {2}", nameof(ProductService), nameof(GetProductAsync), ex.Message);
            return new ItemResult<ProductResponse>
            {
                HasError = true,
                Error = $"Error getting the Product {identifier}."
            };
        }
    }
}