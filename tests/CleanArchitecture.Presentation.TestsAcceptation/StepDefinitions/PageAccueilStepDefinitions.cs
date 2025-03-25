using FluentAssertions;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using CleanArchitecture.Presentation.TestsAcceptation.Support;

namespace CleanArchitecture.Presentation.TestsAcceptation.StepDefinitions
{
    [Binding]
    public class PageAccueilStepDefinitions
    {
        private readonly ScenarioContext? _scenarioContext;
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;

        public PageAccueilStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = EnvironmentConfig.IsHeadless,
                SlowMo = EnvironmentConfig.SlowMoMs
            });
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _page!.CloseAsync();
            await _browser!.CloseAsync();
            _playwright!.Dispose();
        }

        [When(@"j'ouvre la page d'accueil")]
        public async Task QuandJOuvreLaPageDaccueil()
        {
            _page = await _browser!.NewPageAsync();
            await _page.GotoAsync(EnvironmentConfig.BaseUrl);
            // Attendre que la page soit complètement chargée
            await _page.WaitForSelectorAsync(".App-main");
        }

        [Then(@"je dois voir le titre ""(.*)""")]
        public async Task AlorsJeDoisVoirLeTitre(string titre)
        {
            var titreElement = await _page!.QuerySelectorAsync(".card-title");
            var texte = await titreElement!.TextContentAsync();
            texte!.Should().Be(titre);
        }

        [Then(@"je dois voir le texte ""(.*)""")]
        public async Task AlorsJeDoisVoirLeTexte(string texte)
        {
            var welcomeText = await _page!.QuerySelectorAsync(".welcome-text");
            var contenu = await welcomeText!.TextContentAsync();
            contenu!.Should().Be(texte);
        }

        [Then(@"je dois voir les informations sur la technologie ""(.*)""")]
        public async Task AlorsJeDoisVoirLesInformationsSurLaTechnologie(string techno)
        {
            var infoElements = await _page!.QuerySelectorAllAsync(".card-info");
            var found = false;
            
            foreach (var element in infoElements)
            {
                var contenu = await element.TextContentAsync();
                if (contenu!.Contains("Technologie:") && contenu.Contains(techno))
                {
                    found = true;
                    break;
                }
            }
            
            found.Should().BeTrue($"Les informations sur la technologie {techno} devraient être affichées");
        }

        [Then(@"je dois voir les informations sur l'architecture ""(.*)""")]
        public async Task AlorsJeDoisVoirLesInformationsSurLArchitecture(string architecture)
        {
            var infoElements = await _page!.QuerySelectorAllAsync(".card-info");
            var found = false;
            
            foreach (var element in infoElements)
            {
                var contenu = await element.TextContentAsync();
                if (contenu!.Contains("Architecture:") && contenu.Contains(architecture))
                {
                    found = true;
                    break;
                }
            }
            
            found.Should().BeTrue($"Les informations sur l'architecture {architecture} devraient être affichées");
        }
    }
}