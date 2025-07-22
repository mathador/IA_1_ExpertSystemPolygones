using ExpertSystemPCL;
using ExpertSystemPCL.Interfaces;
using System.IO;

namespace Polygones;

internal static class Program
{
    private const string FICHIER_RULES = "rules.rls";

    static void Main()
    {
        // Moteur
        Console.WriteLine("** Création du moteur **");
        Motor m = new(AskBoolValue, AskIntValue); /// TODO ce mode de fonctionnement est temporaire, il faut le remplacer mais par quoi ? Le motor doit-il utiliser l'IHM ?

        // récupération des règles en base de données ou dans un fichier
        string[] rules;
        if (File.Exists(FICHIER_RULES))
        {
            Console.WriteLine($"** Chargement des règles depuis le fichier {FICHIER_RULES} **");
            rules = File.ReadAllLines(FICHIER_RULES);
        }
        else
        {
            Console.WriteLine($"** Aucune règle trouvée dans le fichier {FICHIER_RULES} **");
            return;
        }
        Console.WriteLine("** Lectures des Règles **");


        // Règles
        Console.WriteLine("** Ajout des règles **");
        foreach (var item in rules)
        {
            m.AddRule(item);
        }

        // Résolution
        do
        {
            Console.WriteLine($"{Environment.NewLine}** Résolution **");
            PrintFacts(m.Solve());
        }
        while (AskBoolValue("Voulez-vous continuer ?"));
    }

    public static int AskIntValue(string p)
    {
        Console.WriteLine(p);
        try
        {
            return int.Parse(Console.ReadLine());
        }
        catch
        {
            return 0;
        }
    }

    public static bool AskBoolValue(string p)
    {
        Console.WriteLine($"{p} (yes, no)");
        string res = Console.ReadLine();
        return res.StartsWith("y", StringComparison.OrdinalIgnoreCase);
    }

    public static void PrintFacts(List<IFact> facts)
    {
        var res = $"Solution(s) trouvée(s) : {Environment.NewLine}{string.Join(Environment.NewLine, facts.Where(x => x.Level > 0).OrderByDescending(x => x.Level))}";
        Console.WriteLine(res);
    }

    public static void PrintRules(List<IRule> rules)
    {
        var res = string.Join(Environment.NewLine, rules);
        Console.WriteLine(res);
    }
}
