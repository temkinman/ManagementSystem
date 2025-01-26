using AutoMapper;
using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.GetProductsByCategoryName;

public class GetProductsByCategoryNameQueryHandler : IQueryHandler<GetProductsByCategoryNameQuery, GetProductsByCategoryNameResult>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryNameQueryHandler(
        IMapper mapper,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<GetProductsByCategoryNameResult> Handle(GetProductsByCategoryNameQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products = await _productRepository.GetItemsByConditionAsync(p => p.Category.Name.ToLower() == query.CategoryName.ToLower(), cancellationToken);
        IEnumerable<ProductDto> productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return new GetProductsByCategoryNameResult(productDtos);
    }
}