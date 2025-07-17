using System;
using System.Collections.Generic;

namespace ExpertSystemPCL
{
    public class Rule
    {
        public List<IFact> Premises { get; set; }
        public IFact Conclusion { get; set; }
        public string Name { get; set; }

        public Rule(string _name, List<IFact> _premises, IFact _conclusion)
        {
            Name = _name;
            Premises = _premises;
            Conclusion = _conclusion;
        }

        public override string ToString()
        {
            return Name + " : IF (" + string.Join(" AND ", Premises) + ") THEN " + Conclusion.ToString();
        }
    }
}
