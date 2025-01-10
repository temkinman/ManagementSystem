using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Interfaces;

public interface ICatalogDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token = default);
}