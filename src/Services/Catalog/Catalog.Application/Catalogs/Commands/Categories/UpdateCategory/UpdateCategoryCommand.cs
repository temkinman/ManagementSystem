using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.Categories.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    string Name
    ) : ICommand<UpdateCategoryResult>;

public record UpdateCategoryResult(CategoryDto Category);

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("CategoryId is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name cannot be empty");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Category name can be no more than 100 symbols");
    }
}
