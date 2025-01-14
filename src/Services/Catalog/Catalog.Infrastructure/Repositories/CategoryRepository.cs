using System.Linq.Expressions;
using BuildingBlocks.Exceptions;
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
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        
        await _catalogDbContext.Categories.AddAsync(category, cancellationToken);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return category;
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(category, nameof(category));
        
        _catalogDbContext.Categories.Update(category);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(Guid.Empty == id, nameof(id));
        
        Category? category = await _catalogDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(category), category);
        }
        
        _catalogDbContext.Categories.Remove(category);
        await _catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}