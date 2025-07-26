using DataAcquisition;
using FormesGeometriques;

///TODO: mettre dans appsettings.json
const string FILE_NAME = "rules.rls";

///TODO: utiliser nuget SpectreCosnsole
Console.WriteLine("Bienvenue dans le solveur de forme géométrique.");

// chargement des règles depuis la base de données
var regles = FileLoader.LoadFile(FILE_NAME);

// initialisation du moteur de règles
var moteur = new MoteurDInferences(regles);

///TODO: lancement du moteur de règles
