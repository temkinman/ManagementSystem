using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityTypeConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.ProductName).IsUnique();
        
        builder.HasOne(x=>x.Category)
            .WithMany(x=>x.Products)
            .HasForeignKey(x=>x.CategoryId);
        
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Description)
            .HasMaxLength(800);
    }
}