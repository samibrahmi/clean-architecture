using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Configuration;

/// <summary>
/// Gestionnaire de secrets utilisant Azure Key Vault
/// </summary>
public class KeyVaultSecretManager : ISecretManager
{
    private const string DatabaseConnectionStringKey = "DatabaseConnectionString";
    
    private readonly SecretClient _secretClient;
    private readonly ILogger<KeyVaultSecretManager> _logger;
    
    public KeyVaultSecretManager(SecretClient secretClient, ILogger<KeyVaultSecretManager> logger)
    {
        _secretClient = secretClient;
        _logger = logger;
    }
    
    public async Task<string> GetSecretAsync(string secretName)
    {
        try
        {
            var secret = await _secretClient.GetSecretAsync(secretName);
            return secret.Value.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du secret {SecretName} depuis Key Vault", secretName);
            throw;
        }
    }
    
    public async Task<string> GetDatabaseConnectionStringAsync()
    {
        return await GetSecretAsync(DatabaseConnectionStringKey);
    }
}
