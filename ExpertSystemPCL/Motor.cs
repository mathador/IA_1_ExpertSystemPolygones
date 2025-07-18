using ExpertSystemPCL.Interfaces;
using ExpertSystemPCL.Rules;

namespace ExpertSystemPCL;

public class Motor
{
    private FactsBase fDB;
    private RulesBase rDB;
    private readonly Func<string, bool> _askBoolValue;
    private readonly Func<string, int> _askIntValue;

    public Motor(Func<string, bool> askBoolValue,
                 Func<string, int> askIntValue)
    {
        fDB = new FactsBase();
        rDB = new RulesBase();
        _askBoolValue = askBoolValue;
        _askIntValue = askIntValue;
    }

    public List<IFact> Solve()
    {
        bool moreRules = true;
        RulesBase usableRules = new RulesBase();
        usableRules.Rules = new List<Rule>(rDB.Rules);
        fDB.Clear();

        while (moreRules)
        {
            Tuple<Rule, int> ruleToApply = FindUsableRule(usableRules);
            if (ruleToApply != null)
            {
                IFact newFact = ruleToApply.Item1.Conclusion;
                newFact.SetLevel(ruleToApply.Item2 + 1);
                fDB.AddFact(newFact);
                usableRules.Remove(ruleToApply.Item1);
            }
            else
            {
                moreRules = false;
            }
        }
        return fDB.Facts;
    }

    private int CanApply(Rule r)
    {
        int maxlevel = -1;
        foreach (IFact f in r.Premises)
        {
            IFact foundFact = fDB.Search(f.Name());
            if (foundFact == null)
            {
                if (f.Question() != null)
                {
                    foundFact = FactFactory.Fact(f, this);
                    fDB.AddFact(foundFact);
                    maxlevel = Math.Max(maxlevel, 0);
                }
                else
                {
                    return -1;
                }
            }

            if (!foundFact.Value().Equals(f.Value()))
            {
                return -1;
            }
            else
            {
                maxlevel = Math.Max(maxlevel, foundFact.Level());
            }
        }
        return maxlevel;
    }

    private Tuple<Rule, int> FindUsableRule(RulesBase rBase)
    {
        foreach (Rule r in rBase.Rules)
        {
            int level = CanApply(r);
            if (level != -1)
            {
                return Tuple.Create(r, level);
            }
        }
        return null;
    }

    internal int AskIntValue(string p)
    {
        return _askIntValue(p);
    }

    internal bool AskBoolValue(string p)
    {
        return _askBoolValue(p);
    }

    /// <summary>
    /// Cette méthode permet d'ajouter une règle au moteur.
    /// Mais aussi parser la chaîne de caractères qui représente la règle.
    /// TODO : il faut une séparation des responsabilités, cette méthode ne devrait pas parser la chaîne de caractères.
    /// </summary>
    /// <param name="ruleStr"></param>
    public void AddRule(string ruleStr)
    {
        string[] splitName = ruleStr.Split(new string[] { " : " }, StringSplitOptions.RemoveEmptyEntries);
        if (splitName.Length == 2)
        {
            string name = splitName[0];
            string[] splitPremConcl = splitName[1].Split(new string[] { "IF ", " THEN " }, StringSplitOptions.RemoveEmptyEntries);
            if (splitPremConcl.Length == 2)
            {
                List<IFact> premises = new List<IFact>();
                string[] premisesStr = splitPremConcl[0].Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string prem in premisesStr)
                {
                    IFact premise = FactFactory.Fact(prem);
                    premises.Add(premise);
                }

                string conclusionStr = splitPremConcl[1].Trim();
                IFact conclusion = FactFactory.Fact(conclusionStr);
                rDB.AddRule(new Rule(name, premises, conclusion));
            }
        }
    }
}
