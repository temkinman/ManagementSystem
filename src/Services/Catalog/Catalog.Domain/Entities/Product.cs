namespace Catalog.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime UpdatedDateUtc { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}