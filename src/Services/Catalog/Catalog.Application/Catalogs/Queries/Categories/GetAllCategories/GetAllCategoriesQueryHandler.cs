using AutoMapper;
using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategories;

public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<GetAllCategoriesResult> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories = await _categoryRepository.GetAllAsync(cancellationToken);

        IEnumerable<CategoryDto> categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        
        return new GetAllCategoriesResult(categoryDtos);
    }
}