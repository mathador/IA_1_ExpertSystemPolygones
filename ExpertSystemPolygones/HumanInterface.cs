using System;
using System.Collections.Generic;

namespace ExpertSystemPCL
{
    public interface HumanInterface
    {
        int AskIntValue(String question);
        bool AskBoolValue(String question);
        void PrintFacts(List<IFact> facts);
        void PrintRules(List<Rule> rules);
    }
}
