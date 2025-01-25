using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.GetProductById;

public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public GetProductByIdHandler(
        IMapper mapper,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetItemByConditionAsync(p => p.Id == query.ProductId, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Product), query.ProductId);
        }
        
        ProductDto productDto = _mapper.Map<ProductDto>(product);
        
        return new GetProductByIdResult(productDto);
    }
}