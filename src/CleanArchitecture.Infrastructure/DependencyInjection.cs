using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Configuration;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

/// <summary>
/// Extension pour l'enregistrement des services de la couche Infrastructure
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Ajouter la configuration Key Vault
        services.AddKeyVaultConfiguration(configuration);
        
        // Configuration de la base de données PostgreSQL
        services.AddDbContext<ApplicationDbContext>((provider, options) => {
            var secretManager = provider.GetRequiredService<ISecretManager>();
            var connectionString = secretManager.GetDatabaseConnectionStringAsync().GetAwaiter().GetResult();
            
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        // Enregistrement de l'interface du contexte de base de données
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Enregistrement des repositories et de l'unité de travail
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Services supplémentaires
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}
