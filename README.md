# Gabarit d'applications Clean Architecture

Ce template permet de créer une structure de projet selon les principes de Clean Architecture pour les applications du MELCCFP.

## Installation

```bash
dotnet new install . 
```
> **Note:** Exécutez cette commande depuis le dossier racine du template (où se trouve le dossier .template.config)

## Utilisation

Pour créer un nouveau projet en utilisant ce template, exécutez la commande suivante:

```bash
dotnet new arch-clean --BusinessDomain Eau --Product Suivi --Component Rapports --ComponentType API --output MonProjet
```

Ou avec les alias courts:
```bash
dotnet new arch-clean -bd Eau -p Suivi -c Rapports -ct API -o MonProjet
```

### Exemples

1. Pour créer un projet pour le domaine d'affaires "Eau", produit "Suivi", composant "Rapports" de type "API" dans le dossier "MonProjet":
    ```bash
    dotnet new arch-clean -bd Eau -p Suivi -c Rapports -ct API -o MonProjet
    ```

2. Pour créer un projet pour le domaine d'affaires "Sol", produit "Analyse", composant "Calculs" de type "LOT" dans le dossier "ProjetSol":
    ```bash
    dotnet new arch-clean -bd Sol -p Analyse -c Calculs -ct LOT -o ProjetSol
    ```

## Paramètres

- `BusinessDomain` (-bd): Nom du domaine d'affaires (ex: Eau, Sol, etc.)
- `Product` (-p): Nom court du produit
- `Component` (-c): Nom du composant du produit
- `ComponentType` (-ct): Type de composant (IU, API, BD, LOT, BC)

## Structure du projet généré

Le template génère une solution avec la structure suivante basée sur Clean Architecture:
- src/
  - Domain
  - Application
  - Infrastructure/ (Couche Infrastructure)
  - Presentation/ (Couche Presentation - API/UI)
- tests **(À venir)**
  - UnitTests/
  - IntegrationTests/

## Dépannage

Si vous rencontrez des erreurs lors de l'installation, essayez d'abord de désinstaller le template:

```bash
dotnet new uninstall . 
```

Puis réinstallez-le avec une des méthodes ci-dessus.
