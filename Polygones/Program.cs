using ExpertSystemPCL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polygones;

class Program : IHumanInterface
{
    static void Main(string[] args)
    {
        Program p = new();
        p.Run();
    }

    public void Run()
    {
        // Moteur
        Console.WriteLine("** Création du moteur **");
        Motor m = new(this);

        // Règles
        Console.WriteLine("** Ajout des règles **");
        m.AddRule("R1 : IF (Ordre=3(Quel est l'ordre ?)) THEN Triangle");
        m.AddRule("R2 : IF (Triangle AND Angle Droit(La figure a-t-elle au moins un angle droit ?)) THEN Triangle Rectangle");
        m.AddRule("R3 : IF (Triangle AND Cotes Egaux=2(Combien la figure a-t-elle de côtés égaux ?)) THEN Triangle Isocèle");
        m.AddRule("R4 : IF (Triangle Rectangle AND Triangle Isocèle) THEN Triangle Rectangle Isocèle");
        m.AddRule("R5 : IF (Triangle AND Cotes Egaux=3(Combien la figure a-t-elle de côtés égaux ?)) THEN Triangle Equilateral");
        m.AddRule("R6 : IF (Ordre=4(Quel est l'ordre ?)) THEN Quadrilatère");
        m.AddRule("R7 : IF (Quadrilatère AND Cotes Paralleles=2(Combien y'a-t-il de côtés parallèles entre eux - 0, 2 ou 4)) THEN Trapeze");
        m.AddRule("R8 : IF (Quadrilatère AND Cotes Paralleles=4(Combien y'a-t-il de côtés parallèles entre eux - 0, 2 ou 4)) THEN Parallélogramme");
        m.AddRule("R9 : IF (Parallélogramme AND Angle Droit(La figure a-t-elle au moins un angle droit ?)) THEN Rectangle");
        m.AddRule("R10 : IF (Parallélogramme AND Cotes Egaux=4(Combien la figure a-t-elle de côtés égaux ?)) THEN Losange");
        m.AddRule("R11 : IF (Rectangle AND Losange THEN Carré");

        // Résolution
        while (true)
        {
            Console.WriteLine("\n** Résolution **");
            m.Solve();
        }
    }

    public int AskIntValue(string p)
    {
        Console.WriteLine(p);
        try
        {
            return int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public bool AskBoolValue(string p)
    {
        Console.WriteLine(p + " (yes, no)");
        string res = Console.ReadLine();
        return (res.Equals("yes"));
    }

    public void PrintFacts(List<IFact> facts)
    {
        var res = "Solution(s) trouvée(s) : \n";
        res += string.Join("\n", facts.Where(x => x.Level() > 0).OrderByDescending(x => x.Level()));
        Console.WriteLine(res);
    }

    public void PrintRules(List<Rule> rules)
    {
        var res = "";
        res = string.Join("\n", rules);
        Console.WriteLine(res);
    }
}
