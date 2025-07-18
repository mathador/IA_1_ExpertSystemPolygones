﻿namespace ExpertSystemPCL;

internal class RulesBase
{
    protected List<Rule> rules;
    public List<Rule> Rules
    {
        get
        {
            return rules;
        }
        set
        {
            rules = value;
        }
    }

    public RulesBase()
    {
        rules = new List<Rule>();
    }

    public void ClearBase()
    {
        rules.Clear();
    }

    public void AddRule(Rule r)
    {
        rules.Add(r);
    }

    public void Remove(Rule r)
    {
        rules.Remove(r);
    }
}
