using System.Reflection;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Contexts;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options), ICatalogDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}