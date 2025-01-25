using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Catalogs.Commands.Categories.DeleteProduct;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? existingCategory = await _categoryRepository.GetItemByConditionAsync(x => x.Id == command.CategoryId, cancellationToken);
        
        if (existingCategory == null)
        {
            return new DeleteCategoryResult(false);
        }
        
        await _categoryRepository.DeleteAsync(existingCategory, cancellationToken);

        return new DeleteCategoryResult(true);
    }
}
