using System.Linq.Expressions;

namespace BuildingBlocks.Interfaces;

public interface IBaseItemRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetItemsByConditionAsync(Expression<Func<T, bool>> conditionExpression, CancellationToken cancellationToken = default);
    Task<T?> GetItemByConditionAsync(Expression<Func<T, bool>> conditionExpression, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T item, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T item, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}