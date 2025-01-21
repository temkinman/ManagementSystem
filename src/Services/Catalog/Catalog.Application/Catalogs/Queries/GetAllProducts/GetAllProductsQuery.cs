using BuildingBlocks.CQRS;
using Catalog.Application.Dtos;

namespace Catalog.Application.Catalogs.Queries.GetAllProducts;

public record GetAllProductsQuery() : IQuery<GetAllProductsResult>;

public record GetAllProductsResult(IEnumerable<ProductDto> Products);