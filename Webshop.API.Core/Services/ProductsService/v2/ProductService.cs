using AutoMapper;
using Webshop.API.Contracts.v2.Common;
using Webshop.API.Contracts.v2.Products.Response;
using Webshop.API.Core.Dal.ProductDal;

namespace Webshop.API.Core.Services.ProductsService.v2;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            return new ListResult<ProductResponse>
            {
                HasError = true,
                Errors = new List<string>()
                {
                    $"Error on service {nameof(ProductService)}, method {nameof(GetProductsAsync)}",
                    ex.Message
                }
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
            return new ItemResult<ProductResponse>
            {
                HasError = true,
                Errors = new List<string>()
                {
                    $"Error on service {nameof(ProductService)}, method {nameof(GetProductAsync)}",
                    ex.Message
                }
            };
        }
    }
}