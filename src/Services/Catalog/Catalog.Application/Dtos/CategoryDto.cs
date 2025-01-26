namespace Catalog.Application.Dtos;

public record CategoryDto(string Name)
{
    public CategoryDto() : this(string.Empty)
    { }
}
