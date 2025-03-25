using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace CleanArchitecture.Presentation.TestsAcceptation.Support
{
    /// <summary>
    /// Classe de hooks pour l'installation et la configuration globale de Playwright
    /// </summary>
    [Binding]
    public class PlaywrightHooks
    {
        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            // Installer les navigateurs Playwright si nécessaire
            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "--with-deps" });
            if (exitCode != 0)
            {
                throw new Exception($"Playwright installation failed with exit code {exitCode}");
            }

            await Task.CompletedTask;
        }
    }
}
