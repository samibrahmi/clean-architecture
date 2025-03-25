using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Configuration;

/// <summary>
/// Extensions pour configurer Azure Key Vault
/// </summary>
public static class KeyVaultConfigurationExtensions
{
    /// <summary>
    /// Ajoute la configuration Azure Key Vault à l'application
    /// </summary>
    public static IServiceCollection AddKeyVaultConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string? keyVaultEndpoint = configuration["KeyVault:Endpoint"];
        
        // Vérifier si la configuration de Key Vault est présente
        if (!string.IsNullOrEmpty(keyVaultEndpoint))
        {
            // Création du client Key Vault
            var secretClient = new SecretClient(
                new Uri(keyVaultEndpoint),
                new DefaultAzureCredential());
            
            // Enregistrement du client dans le conteneur DI
            services.AddSingleton(secretClient);
            
            // Enregistrement du service pour récupérer les secrets
            services.AddScoped<ISecretManager, KeyVaultSecretManager>();
        }
        else
        {
            // Fallback pour le développement local utilisant les variables d'environnement
            services.AddScoped<ISecretManager, EnvironmentSecretManager>();
        }
        
        return services;
    }
}
