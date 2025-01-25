using AutoMapper;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;
using Catalog.Application.Dtos;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Commands.Categories.CreateCategory;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        Category category = _mapper.Map<Category>(command);

        Category? existingCategory = await _categoryRepository.GetItemByConditionAsync(x => x.Id == category.Id, cancellationToken);
        
        if (existingCategory == null)
        {
            throw new NotFoundException(nameof(category), $"Category with this {command.Id} not found.");
        }

        Category? existingCategoryWithRequestName = await _categoryRepository.GetItemByConditionAsync(x => x.Name.ToLower() == category.Name.ToLower(), cancellationToken);

        if (existingCategoryWithRequestName != null)
        {
            throw new ConflictException($"Category with this {command.Name} already exists.");
        }

        Category updatedCategory = await _categoryRepository.UpdateAsync(category, cancellationToken);
        CategoryDto categoryDto = _mapper.Map<CategoryDto>(updatedCategory);
        
        return new UpdateCategoryResult(categoryDto);
    }
}