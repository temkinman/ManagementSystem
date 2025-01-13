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
    
    public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetItemsByConditionAsync(Expression<Func<Product, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetItemByConditionAsync(Expression<Func<Product, bool>> conditionExpression, CancellationToken cancellationToken)
    {
        return  await _catalogDbContext.Products.FirstOrDefaultAsync(conditionExpression, cancellationToken);
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }
        
        await _catalogDbContext.Products.AddAsync(product, cancellationToken);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return product;
    }

    public Task<Product> UpdateAsync(Product item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}