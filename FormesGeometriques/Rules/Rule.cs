namespace FormesGeometriques;

public class Rule
{
    public string Name { get; set; }
    public IEnumerable<Fait> Premisses { get; set; }
    public string Conclusion { get; set; }

    internal string GetConclusion()
    {
        throw new NotImplementedException();
    }

    internal bool IsApplicable()
    {
        throw new NotImplementedException();
    }
}