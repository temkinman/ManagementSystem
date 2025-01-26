using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;

namespace Catalog.Application.Catalogs.Queries.Categories.GetAllCategories;

public record GetAllCategoriesQuery() : IQuery<GetAllCategoriesResult>;

public record GetAllCategoriesResult(IEnumerable<CategoryDto> Categories);