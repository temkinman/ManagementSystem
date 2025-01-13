namespace Catalog.Application.Dtos;

public record ProductDto(
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    DateTime CreatedDateUtc,
    DateTime UpdatedDateUtc,
    CategoryDto? CategoryDto
);