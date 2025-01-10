namespace Catalog.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public ICollection<Product>? Products { get; set; }
}