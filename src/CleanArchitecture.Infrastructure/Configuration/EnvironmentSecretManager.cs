using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration;

/// <summary>
/// Gestionnaire de secrets utilisant les variables d'environnement (fallback)
/// </summary>
public class EnvironmentSecretManager : ISecretManager
{
    private readonly IConfiguration _configuration;
    
    public EnvironmentSecretManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task<string> GetSecretAsync(string secretName)
    {
        // Rechercher d'abord dans les variables d'environnement avec format normalisé
        var envVarName = secretName.Replace(":", "__");
        var value = Environment.GetEnvironmentVariable(envVarName);
        
        if (string.IsNullOrEmpty(value))
        {
            // Essayer de récupérer depuis la configuration
            value = _configuration[secretName];
        }
        
        return Task.FromResult(value ?? string.Empty);
    }
    
    public Task<string> GetDatabaseConnectionStringAsync()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return Task.FromResult(connectionString ?? string.Empty);
    }
}
