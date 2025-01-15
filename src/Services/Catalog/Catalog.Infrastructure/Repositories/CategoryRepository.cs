using System.Linq.Expressions;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _catalogDbContext;

    public CategoryRepository(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetItemsByConditionAsync(Expression<Func<Category, bool>> conditionExpression, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Categories.Where(conditionExpression)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetItemByConditionAsync(Expression<Func<Category, bool>> conditionExpression, CancellationToken cancellationToken = default)
    {
        return await _catalogDbContext.Categories.FirstOrDefaultAsync(conditionExpression, cancellationToken);
    }

    public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _catalogDbContext.Categories.AddAsync(category, cancellationToken);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return category;
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        _catalogDbContext.Categories.Update(category);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<bool> DeleteAsync(Category category, CancellationToken cancellationToken = default)
    {
        _catalogDbContext.Categories.Remove(category);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}