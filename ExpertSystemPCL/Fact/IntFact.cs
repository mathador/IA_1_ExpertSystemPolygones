using ExpertSystemPCL.Interfaces;

namespace ExpertSystemPCL;

internal class IntFact : IFact
{
    protected string name;
    public string Name => name;

    protected int value;
    public object Value => value;

    protected int level;
    public int Level => level;
    public void SetLevel(int l)
    {
        level = l;
    }

    protected string question;
    public string Question => question;

    public IntFact(string _name, int _value, string _question, int _level = 0)
    {
        name = _name;
        value = _value;
        question = _question;
        level = _level;
    }

    //public override string ToString()
    //{
    //    return name + "=" + value + " (" + level + ")";
    //}
}
