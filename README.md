# Syst�me Expert de Reconnaissance de Polygones

### En cas d'erreur de build:

Si l'erreur:
```
Your project does not reference ".NETFramework,Version=v4.8" framework. Add a reference to ".NETFramework,Version=v4.8" in the "TargetFrameworks" property of your project file and then re-run NuGet restore.
```
appara�t il se peut qu'il suffise de supprimer le dossier 'bin' e ou 'obj' du projet **Polygones**.

## Description du projet

Cette solution est bas�e sur les sources en ligne du livre de Virginie MATHIVET � *L'IA pour les d�v CSharp* (�ditions ENI).

- Langage :  C#
- Framework :  .NET 4.8 (initialement .NET 4.5)

La solution comprend deux projets�:

- **Polygones** :
  - Projet principal
  - Contient la logique de l'interface graphique (console) et d'autres fonctionnalit�s
- **ExpertSystemPCL** :
  - Librairie portable
  - Contient la logique du moteur de faits et de r�gles du syst�me expert

---

## Description de l'exercice

L'objectif de l'exercice est d'impl�menter un **syst�me expert** pour la reconnaissance de polygones.

Le syst�me doit �tre capable de reconna�tre diff�rents types de polygones en fonction de leurs propri�t�s g�om�triques.

---

## Tests

Un exemple de test unitaire, mal form� a �t� ajout� pour montrer que cette "architecture" ne permet pas d'en faire et par cons�quent, toute modificaion du projet de base peut amener � une r�gression

> *Projet p�dagogique � Intelligence Artificielle, syst�mes experts, C#*