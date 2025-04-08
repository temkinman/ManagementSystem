using System.Linq.Expressions;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogDbContext _catalogDbContext;
    public ProductRepository(ICatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _catalogDbContext.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetItemsByConditionAsync(Expression<Func<Product, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return await _catalogDbContext.Products.Where(conditionExpression)
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetItemByConditionAsync(Expression<Func<Product, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return  await _catalogDbContext.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(conditionExpression, cancellationToken);
    }

    public Task<Product?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        product.Id = Guid.NewGuid();
        product.CreatedDateUtc = DateTime.UtcNow;
        product.UpdatedDateUtc = DateTime.UtcNow;
        
        await _catalogDbContext.Products.AddAsync(product, cancellationToken);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        product.UpdatedDateUtc = DateTime.UtcNow;
        
        _catalogDbContext.Products.Update(product);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);

        return product;
    }

    public async Task<bool> DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _catalogDbContext.Products.Remove(product);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}