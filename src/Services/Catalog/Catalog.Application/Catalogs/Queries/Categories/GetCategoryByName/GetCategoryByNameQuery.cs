using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryByName;

public record GetCategoryByNameQuery(string CategoryName) : IQuery<GetCategoryByNameResult>;

public record GetCategoryByNameResult(CategoryDto Category);

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByNameQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category's name cannot be empty");
        RuleFor(x => x.CategoryName).MaximumLength(100).WithMessage("Category's name can be no more than 100 symbols");
    }
}