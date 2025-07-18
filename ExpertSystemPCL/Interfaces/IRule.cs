namespace ExpertSystemPCL.Interfaces;

public interface IRule
{
    IFact Conclusion { get; set; }
    string Name { get; set; }
    List<IFact> Premises { get; set; }
}