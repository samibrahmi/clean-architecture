# Tests d'Acceptation pour l'Interface Utilisateur

Ce projet contient des tests d'acceptation automatisés qui utilisent [SpecFlow](https://specflow.org/), [XUnit](https://xunit.net/) et [Playwright](https://playwright.dev/) pour valider l'interface utilisateur web de l'application.

## Prérequis

- .NET 9.0 SDK ou supérieur
- Un navigateur web (Chrome, Firefox, ou Edge)

## Installation

Les dépendances nécessaires seront restaurées automatiquement lors de la construction du projet.
Playwright installera automatiquement les navigateurs requis lors de la première exécution.

## Exécution des tests
### Depuis Visual Studio

1. Démarrez d'abord l'application web :
   - Clic droit sur le projet `CleanArchitecture.Presentation` → Définir comme projet de démarrage
   - Appuyez sur F5 ou cliquez sur le bouton "Démarrer"
   - Vérifiez que l'application est accessible sur https://localhost:3000

2. Exécutez les tests d'acceptation :
   - Ouvrez l'Explorateur de tests (Test → Explorateur de tests)
   - Cliquez sur "Exécuter tout" ou sélectionnez des tests spécifiques à exécuter

3. Pour déboguer un test :
   - Définissez des points d'arrêt dans vos step definitions
   - Clic droit sur le test → Déboguer


### Depuis Visual Studio Code

1. Démarrez l'application web :
   ```bash
   dotnet run --project ../../src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj
   ```

2. Dans une autre fenêtre de terminal, exécutez les tests :
   ```bash
   dotnet test
   ```

3. Pour exécuter avec l'extension .NET Test Explorer :
   - Installez l'extension ".NET Core Test Explorer"
   - Configurez-la pour pointer vers le projet de tests
   - Utilisez l'interface graphique pour exécuter/déboguer les tests

## Configuration

Vous pouvez configurer l'environnement de test avec les variables d'environnement suivantes :

- `TEST_BASE_URL` : URL de base pour l'application (par défaut : http://localhost:5293)
- `TEST_HEADLESS` : Exécuter les navigateurs en mode headless (par défaut : false)
- `TEST_SLOWMO` : Ralentir l'exécution des tests en millisecondes (par défaut : 300)

Exemples :

```bash
# Windows
set TEST_HEADLESS=true
set TEST_SLOWMO=100
dotnet test

# Linux/MacOS
TEST_HEADLESS=true TEST_SLOWMO=100 dotnet test
```

## Dépannage

### Les tests échouent à se connecter à l'application

- Vérifiez que l'application web est bien en cours d'exécution
- Confirmez l'URL dans `EnvironmentConfig.cs` (https://localhost:3000 par défaut)
- Vérifiez les erreurs de console du navigateur en désactivant le mode headless

### Échec d'installation des navigateurs Playwright

Si vous rencontrez des erreurs lors de l'installation des navigateurs :

```bash
# Installation manuelle des navigateurs
pwsh -c "npx playwright install --with-deps"
# ou
pwsh -c "npx playwright install chromium"
```

## Structure du projet

- `Features/` : Contient les fichiers .feature écrits en Gherkin
- `StepDefinitions/` : Implémentations C# des étapes définies dans les fichiers .feature
- `Support/` : Classes d'assistance pour la configuration et les opérations communes

## Ajouter de nouveaux tests

1. Créez un nouveau fichier .feature dans le dossier `Features`
2. Définissez vos scénarios en utilisant la syntaxe Gherkin
3. Implémentez les étapes dans une nouvelle classe de step definitions
4. Exécutez les tests pour vérifier votre implémentation