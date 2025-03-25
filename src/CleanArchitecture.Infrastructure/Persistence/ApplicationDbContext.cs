using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Contexte de base de données principal de l'application utilisant EF Core
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ICurrentUserService? _currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
        ICurrentUserService? currentUserService = null) : base(options)
    {
        _currentUserService = currentUserService;
    }

    // Exemple d'une DbSet pour l'entité Example
    public DbSet<Example> Examples => Set<Example>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatisation de l'audit trail (Created/Modified)
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreationInfo(_currentUserService?.UserId ?? "System");
                    break;
                    
                case EntityState.Modified:
                    entry.Entity.SetModificationInfo(_currentUserService?.UserId ?? "System");
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Application de toutes les configurations d'entités
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}
