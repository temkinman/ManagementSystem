using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryByName;

public class GetCategoryByNameHandler : IQueryHandler<GetCategoryByNameQuery, GetCategoryByNameResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public GetCategoryByNameHandler(
        IMapper mapper,
        ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<GetCategoryByNameResult> Handle(GetCategoryByNameQuery query, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetItemByConditionAsync(p => p.Name.ToLower() == query.CategoryName.ToLower(), cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(category), query.CategoryName);
        }
        
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);
        
        return new GetCategoryByNameResult(categoryDto);
    }
}