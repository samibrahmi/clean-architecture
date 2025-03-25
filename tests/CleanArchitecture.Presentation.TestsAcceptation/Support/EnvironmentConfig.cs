using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Presentation.TestsAcceptation.Support
{
    /// <summary>
    /// Classe pour gérer la configuration de l'environnement de test
    /// </summary>
    public static class EnvironmentConfig
    {
        private static readonly IConfiguration Configuration;

        static EnvironmentConfig()
        {
            // Charger la configuration à partir du appsettings.json du projet principal
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string BaseUrl
        {
            get
            {
                // Utiliser la variable d'environnement si définie, sinon lire depuis la configuration
                var envUrl = Environment.GetEnvironmentVariable("TEST_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                    return envUrl;

                var httpsPort = Configuration.GetValue<int>("ApplicationUrls:HttpsPort", 7063);
                return $"https://localhost:{httpsPort}";
            }
        }
        
        public static bool IsHeadless => string.Equals(
            Environment.GetEnvironmentVariable("TEST_HEADLESS"), 
            "true", 
            StringComparison.OrdinalIgnoreCase);
        
        public static int SlowMoMs => int.TryParse(
            Environment.GetEnvironmentVariable("TEST_SLOWMO"), 
            out var slowMo) ? slowMo : 300;
    }
}
