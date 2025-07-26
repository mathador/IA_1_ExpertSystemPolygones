# Système Expert de Reconnaissance de Polygones

### En cas d'erreur de build:

Si l'erreur:
```
Your project does not reference ".NETFramework,Version=v4.8" framework. Add a reference to ".NETFramework,Version=v4.8" in the "TargetFrameworks" property of your project file and then re-run NuGet restore.
```
apparaît il se peut qu'il suffise de supprimer le dossier 'bin' e ou 'obj' du projet **Polygones**.

## Description du projet

Cette solution est basée sur les sources en ligne du livre de Virginie MATHIVET — *L'IA pour les dév CSharp* (éditions ENI).

- Langage :  C#
- Framework :  .NET 4.8 (initialement .NET 4.5)

La solution comprend deux projets :

- **Polygones** :
  - Projet principal
  - Contient la logique de l'interface graphique (console) et d'autres fonctionnalités
- **ExpertSystemPCL** :
  - Librairie portable
  - Contient la logique du moteur de faits et de règles du système expert

---

## Description de l'exercice

L'objectif de l'exercice est d'implémenter un **système expert** pour la reconnaissance de polygones.

Le système doit être capable de reconnaître différents types de polygones en fonction de leurs propriétés géométriques.

---

## Tests

Un exemple de test unitaire, mal formé a été ajouté pour montrer que cette "architecture" ne permet pas d'en faire et par conséquent, toute modificaion du projet de base peut amener à une régression

> *Projet pédagogique — Intelligence Artificielle, systèmes experts, C#*