using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Queries.GetProductsByCategoryName;

public record GetProductsByCategoryNameQuery(string CategoryName) : IQuery<GetProductsByCategoryNameResult>;

public record GetProductsByCategoryNameResult(IEnumerable<ProductDto> Products);

public class GetProductsByCategoryNameQueryValidator : AbstractValidator<GetProductsByCategoryNameQuery>
{
    public GetProductsByCategoryNameQueryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name cannot be empty");
        RuleFor(x => x.CategoryName).MaximumLength(100).WithMessage("Category name can be no more than 100 symbols");
    }
}