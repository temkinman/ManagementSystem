namespace Catalog.Application.Dtos;

public record ProductDto(
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    CategoryDto? CategoryDto)
{
    public ProductDto() : this(string.Empty, string.Empty, 0m, 0, null)
    { }
};