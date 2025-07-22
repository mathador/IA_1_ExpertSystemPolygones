using ExpertSystemPCL.Interfaces;
using ExpertSystemPCL.Rules;

namespace ExpertSystemPCL;

public class Motor
{
    private readonly FactsBase fDB = new();
    private readonly RulesBase rDB = new();
    private readonly Func<string, bool> _askBoolValue;
    private readonly Func<string, int> _askIntValue;

    public Motor(Func<string, bool> askBoolValue,
                 Func<string, int> askIntValue)
    {
        _askBoolValue = askBoolValue;
        _askIntValue = askIntValue;
    }

    public List<IFact> Solve()
    {
        RulesBase usableRules = new()
        {
            Rules = [.. rDB.Rules]
        };

        Tuple<Rule, int>? ruleToApply;
        fDB.Clear();
        do
        {
            ruleToApply = FindUsableRule(usableRules);
            if (ruleToApply != null)
            {
                IFact newFact = ruleToApply.Item1.Conclusion;
                newFact.SetLevel(ruleToApply.Item2 + 1);
                fDB.AddFact(newFact);
                usableRules.Remove(ruleToApply.Item1);
            }
        }
        while (ruleToApply is not null);

        return fDB.Facts;
    }

    private int CanApply(Rule r)
    {
        int maxlevel = -1;
        foreach (IFact f in r.Premises)
        {
            IFact foundFact = fDB.Search(f.Name);
            if (foundFact == null)
            {
                if (f.Question != null)
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

            if (!foundFact.Value.Equals(f.Value))
            {
                return -1;
            }
            else
            {
                maxlevel = Math.Max(maxlevel, foundFact.Level);
            }
        }
        return maxlevel;
    }

    private Tuple<Rule, int>? FindUsableRule(RulesBase rBase)
    {
        foreach (var r in rBase.Rules)
        {
            var level = CanApply(r);
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
        var splitName = ruleStr.Split([" : "], StringSplitOptions.RemoveEmptyEntries);
        if (splitName.Length == 2)
        {
            var name = splitName[0];
            var splitPremConcl = splitName[1].Split(["IF ", " THEN "], StringSplitOptions.RemoveEmptyEntries);
            if (splitPremConcl.Length == 2)
            {
                List<IFact> premises = [];
                var premisesStr = splitPremConcl[0].Split([" AND "], StringSplitOptions.RemoveEmptyEntries);
                foreach (var prem in premisesStr)
                {
                    IFact premise = FactFactory.Fact(prem);
                    premises.Add(premise);
                }

                var conclusionStr = splitPremConcl[1].Trim();
                IFact conclusion = FactFactory.Fact(conclusionStr);
                rDB.AddRule(new(name, premises, conclusion));
            }
        }
    }
}
