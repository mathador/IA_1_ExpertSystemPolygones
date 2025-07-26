namespace FormesGeometriques;

public static class RuleParser
{
    private const string SEPARATOR = " : ";
    private const string IF = "IF";
    private const string THEN = "THEN";
    private const string AND = "AND";
    private const int MIN_PART_NUMBER = 3;

    private static string[] ALL_SEPARATORS = [SEPARATOR, IF, THEN, AND];

    public static Rule Parse(string s)
    {
        // Guard clauses
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("La règle à parser ne peut êter nulle ou vide.");
        }

        if (!s.Contains(SEPARATOR))
        {
            throw new ArgumentException($"La règle à parser doit contenir un nom séparer de la règle par '{SEPARATOR}'.");
        }

        if (!s.Contains(IF))
        {
            throw new ArgumentException($"La règle à parser doit contenir une condition commençant par '{IF}'.");
        }

        if (!s.Contains(THEN))
        {
            throw new ArgumentException($"La règle à parser doit contenir une conclusion commençant par '{THEN}'.");
        }

        var separated = s.Split(ALL_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
        if (separated.Length < MIN_PART_NUMBER)
        {
            throw new ArgumentException($"La règle à parser doit contenir un nom, au moin une condition et une conclusion.");
        }

        // traitement
        var rule = new Rule
        {
            Name = separated[0].Trim(),
            Premisses = FaitHelpers.GetPremisses(separated),
            Conclusion = separated[^1].Trim()
        };
        return rule;
    }


}
