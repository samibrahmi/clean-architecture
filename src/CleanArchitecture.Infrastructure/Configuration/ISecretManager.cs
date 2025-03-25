namespace CleanArchitecture.Infrastructure.Configuration;

/// <summary>
/// Interface pour gérer les secrets de l'application
/// </summary>
public interface ISecretManager
{
    /// <summary>
    /// Récupère un secret par son nom
    /// </summary>
    Task<string> GetSecretAsync(string secretName);
    
    /// <summary>
    /// Récupère la chaîne de connexion à la base de données
    /// </summary>
    Task<string> GetDatabaseConnectionStringAsync();
}
