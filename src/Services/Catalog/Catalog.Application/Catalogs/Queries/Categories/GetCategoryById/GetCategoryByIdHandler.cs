using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryById;

public class GetCategoryByIdHandler : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public GetCategoryByIdHandler(
        IMapper mapper,
        ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetItemByConditionAsync(p => p.Id == query.CategoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(category), query.CategoryId);
        }
        
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        
        return new GetCategoryByIdResult(categoryDto);
    }
}