using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Common.Interfaces;

/// <summary>
/// Interface générique de dépôt pour les opérations CRUD
/// </summary>
public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
