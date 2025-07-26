namespace FormesGeometriques;
public class MoteurDInferences
{
    private List<Rule> rDB = [];
    private List<string> conclusionsRetenues = [];

    public MoteurDInferences(string[] rules)
    {
        InitializeMoteur(rules);
    }

    public void InitializeMoteur(string[] rules)
    {
        rDB.Clear();
        conclusionsRetenues.Clear();
        foreach (var rule in rules)
        {
            rDB.Add(RuleParser.Parse(rule));
        }
    }

    public IEnumerable<Rule> GetRulesFor(Fait f)
    {
        return f switch
        {
            IntFait intFait => rDB.Where(r => r.Premisses.Any(p => p is IntFait i && i.Valeur == intFait.Valeur)),
            BoolFait bFait => rDB.Where(r => r.Premisses.Any(p => p is BoolFait b && b.Valeur == bFait.Valeur)),
            _ => GetRulesForFait(f)
        };
    }

    private IEnumerable<Rule> GetRulesForFait(Fait f)
    {
        if (string.IsNullOrWhiteSpace(f.Nom)) f.Nom = "Ordre";
        return rDB.Where(r => r.Premisses.Any(p => p.Nom.Equals(f.Nom)));
    }
}
