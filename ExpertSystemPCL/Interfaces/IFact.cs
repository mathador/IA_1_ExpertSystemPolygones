namespace ExpertSystemPCL.Interfaces;

public interface IFact
{
    string Name { get; }

    object Value { get; }

    int Level { get; }

    string Question { get; }

    void SetLevel(int p);
}
