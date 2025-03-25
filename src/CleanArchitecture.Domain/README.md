# Classes de Base pour les Entités du Domaine

Ce projet contient deux classes de base pour les entités:

1. **EntityBase** (Common/EntityBase.cs)
   - Utilisez pour les entités qui nécessitent une piste d'audit (suivi de création/modification)
   - Fournit des propriétés pour suivre qui a créé/modifié les enregistrements et quand

2. **Entity** (Entities/Entity.cs)
   - Hérite de EntityBase et ajoute les méthodes de comparaison d'égalité
   - Combine les fonctionnalités d'audit et les mécanismes d'identité/égalité
