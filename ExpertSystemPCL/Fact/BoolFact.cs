using ExpertSystemPCL.Interfaces;

namespace ExpertSystemPCL;

internal class BoolFact : IFact
{
    protected string name;
    public string Name => name;

    protected bool value;
    public object Value => value;

    protected int level;
    public int Level => level;
    public void SetLevel(int l)
    {
        level = l;
    }

    protected string question;
    public string Question => question;

    public BoolFact(string _name, bool _value, string _question = null, int _level = 0)
    {
        name = _name;
        value = _value;
        question = _question;
        level = _level;
    }

    //public override string ToString()
    //{
    //    string res = "";
    //    if (!value)
    //    {
    //        res += "!";
    //    }
    //    res += name + " (" + level + ")";
    //    return res;
    //}
}
