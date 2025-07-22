using ExpertSystemPCL.Interfaces;

namespace ExpertSystemPCL;

internal class FactsBase
{
    protected List<IFact> facts;
    public List<IFact> Facts
    {
        get
        {
            return facts;
        }
    }

    public FactsBase()
    {
        facts = [];
    }

    public void Clear()
    {
        facts.Clear();
    }

    public void AddFact(IFact f)
    {
        facts.Add(f);
    }

    public IFact Search(string _name)
    {
        return facts.FirstOrDefault(x => x.Name.Equals(_name, StringComparison.Ordinal));
    }

    public object Value(string _name)
    {
        IFact f = facts.FirstOrDefault(x => x.Name.Equals(_name, StringComparison.Ordinal));
        if (f != null)
        {
            return f.Value;
        }
        else
        {
            return null;
        }
    }
}
