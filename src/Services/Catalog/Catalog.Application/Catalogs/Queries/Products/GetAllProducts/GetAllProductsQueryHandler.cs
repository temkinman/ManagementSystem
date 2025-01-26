using AutoMapper;
using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public GetAllProductsQueryHandler(
        IMapper mapper,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await _productRepository.GetAllAsync(cancellationToken);

        IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return new GetAllProductsResult(productDtos);
    }
}