using BuildingBlocks.CQRS;
using FluentValidation;

namespace Catalog.Application.Catalogs.Commands.Categories.CreateCategory;

public record CreateCategoryCommand(string CategoryName) : ICommand<CreateCategoryResult>;

public record CreateCategoryResult(Guid Id);

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name cannot be empty");
        RuleFor(x => x.CategoryName).MaximumLength(100).WithMessage("Category name can be no more than 100 symbols");
    }
}