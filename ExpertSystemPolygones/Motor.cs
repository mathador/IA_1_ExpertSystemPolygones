using System;
using System.Collections.Generic;

namespace ExpertSystemPCL
{
    public class Motor
    {
        private FactsBase fDB;
        private RulesBase rDB;
        private HumanInterface ihm;

        public Motor(HumanInterface _ihm)
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
                if (ruleToApply!= null)
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
                if (foundFact == null) {
                    if (f.Question() != null) {
                        foundFact = FactFactory.Fact(f, this);
                        fDB.AddFact(foundFact);
                        maxlevel = Math.Max(maxlevel, 0);
                    }
                    else {
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

        private Tuple<Rule, int> FindUsableRule(RulesBase rBase) {
            foreach(Rule r in rBase.Rules) {
                int level = CanApply(r);
                if (level != -1) {
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
            String[] splitName = ruleStr.Split(new String[] {" : "}, StringSplitOptions.RemoveEmptyEntries);
            if (splitName.Length == 2)
            {
                String name = splitName[0];
                String[] splitPremConcl = splitName[1].Split(new String[] {"IF ", " THEN " }, StringSplitOptions.RemoveEmptyEntries);
                if (splitPremConcl.Length == 2)
                {
                    List<IFact> premises = new List<IFact>();
                    String[] premisesStr = splitPremConcl[0].Split(new String[] {" AND "}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String prem in premisesStr)
                    {
                        IFact premise = FactFactory.Fact(prem);
                        premises.Add(premise);
                    }

                    String conclusionStr = splitPremConcl[1].Trim();
                    IFact conclusion = FactFactory.Fact(conclusionStr);
                    rDB.AddRule(new Rule(name, premises, conclusion));
                }
            }
        }
    }
}
