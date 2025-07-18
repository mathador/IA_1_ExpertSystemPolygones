using ExpertSystemPCL.Interfaces;

namespace Polygones.Interfaces;
public interface IHumanInterface
{
    int AskIntValue(string question);
    bool AskBoolValue(string question);
    void PrintFacts(List<IFact> facts);
    void PrintRules(List<IRule> rules);
}
