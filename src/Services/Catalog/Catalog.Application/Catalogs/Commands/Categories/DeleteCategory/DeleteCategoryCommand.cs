using BuildingBlocks.CQRS;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.Categories.DeleteProduct;

public record DeleteCategoryCommand(Guid CategoryId) : ICommand<DeleteCategoryResult>;

public record DeleteCategoryResult(bool IsSuccess);

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
    }
}