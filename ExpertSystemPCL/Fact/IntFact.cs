using ExpertSystemPCL.Interfaces;

namespace ExpertSystemPCL;

internal class IntFact : IFact
{
    protected string name;
    public string Name()
    {
        return name;
    }

    protected int value;
    public object Value()
    {
        return value;
    }

    protected int level;
    public int Level()
    {
        return level;
    }
    public void SetLevel(int l)
    {
        level = l;
    }

    protected string question = null;
    public string Question()
    {
        return question;
    }

    public IntFact(string _name, int _value, string _question = null, int _level = 0)
    {
        name = _name;
        value = _value;
        question = _question;
        level = _level;
    }

    public override string ToString()
    {
        return name + "=" + value + " (" + level + ")";
    }
}
