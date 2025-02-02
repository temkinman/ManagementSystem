using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Commands.Categories.CreateCategory;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        Category category = _mapper.Map<Category>(command);

        Category? existingCategory = await _categoryRepository.GetItemByConditionAsync(x => x.Name.ToLower() == category.Name.ToLower(), cancellationToken);
        
        if (existingCategory != null)
        {
            throw new ConflictException("Category with this name already exists.");
        }

        Category addedCategory = await _categoryRepository.CreateAsync(category, cancellationToken);
        
        return new CreateCategoryResult(addedCategory.Id);
    }
}