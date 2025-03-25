# Guide de Configuration des Secrets

Ce document explique comment configurer et gérer les secrets dans l'application Clean Architecture.

## Options de Configuration

L'application offre deux méthodes pour gérer les secrets et configurations sensibles :

1. **Variables d'environnement** (développement local et déploiements simples)
2. **Azure Key Vault** (environnements de production)

## Configuration avec Variables d'Environnement

### En développement local

1. Créez un fichier `.env` à la racine du projet (assurez-vous qu'il est dans .gitignore)

