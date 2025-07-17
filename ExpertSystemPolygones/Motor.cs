using System;
using System.Collections.Generic;

namespace ExpertSystemPCL
{
    public class Motor
    {
        private FactsBase fDB;
        private RulesBase rDB;
        private IHumanInterface ihm;

        public Motor(IHumanInterface _ihm)
        {
            ihm = _ihm;
            fDB = new FactsBase();
            rDB = new RulesBase();
        }

        public void Solve()
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
            ihm.PrintFacts(fDB.Facts);
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
            return ihm.AskIntValue(p);
        }

        internal bool AskBoolValue(string p)
        {
            return ihm.AskBoolValue(p);
        }

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
}
