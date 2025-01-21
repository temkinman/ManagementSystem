using Catalog.Application.Dtos;

namespace Catalog.Api.Dto;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    Guid? CategoryId);

public record UpdateProductResponse(ProductDto ProductDto);
