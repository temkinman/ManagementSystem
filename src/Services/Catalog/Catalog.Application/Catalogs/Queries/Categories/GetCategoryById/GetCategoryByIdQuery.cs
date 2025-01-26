using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;
using FluentValidation;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategoryById;

public record GetCategoryByIdQuery(Guid CategoryId) : IQuery<GetCategoryByIdResult>;

public record GetCategoryByIdResult(CategoryDto Category);

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
    }
}