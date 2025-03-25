namespace CleanArchitecture.Application.Common.Interfaces;

/// <summary>
/// Interface pour le contexte de base de données de l'application
/// Implémentez cette interface dans la couche Infrastructure
/// </summary>
public interface IApplicationDbContext
{
    // Exemple: DbSet<VotreEntite> VosEntites { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
