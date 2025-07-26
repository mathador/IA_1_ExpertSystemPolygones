using System.Collections.Immutable;

namespace FormesGeometriques;

public static class FaitHelpers
{

    private const string PARENTHESE_FERMANTE = ")";
    private static string[] PREMISSES_SEPARATOR = ["(", "="];

    public static Fait BuildFait(string[] question)
    {
        if (question == null || question.Length == 0)
            throw new ArgumentException("Un fait ne peut pas être construit à partir d'une chaîne vide ou nulle.");

        Fait fait;
        switch (question.Length)
        {
            case 1:
                fait = new Fait();
                break;
            case 2: ///TODO: convertir en enum ?
                fait = new BoolFait();
                break;
            case 3:
                fait = new IntFait(int.Parse(question[1].Trim()));
                break;
            default:
                throw new ArgumentException("Un fait avec question ne peut contenir que 1, 2 ou 3 parties.");
        }
        fait.Nom = question[0].Trim();
        if (question.Length > 1)
            fait.Question = question[^1].Trim();
        return fait;
    }

    public static ImmutableArray<Fait> GetPremisses(string[] separated)
    {
        var separatedPremisses = separated
                .Skip(1)
                .Take(separated.Length - 2)
                .Select(s => s.Trim())
                .Select(s => s.Replace(PARENTHESE_FERMANTE, string.Empty))
                .Select(s => s.Split(PREMISSES_SEPARATOR, StringSplitOptions.RemoveEmptyEntries));

        List<Fait> premisses = [];
        foreach (var item in separatedPremisses)
        {
            premisses.Add(BuildFait(item));
        }
        return [.. premisses];
    }
}