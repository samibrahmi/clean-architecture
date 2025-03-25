namespace CleanArchitecture.Application.Common.Interfaces;

/// <summary>
/// Interface pour le patron d'unité de travail
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
