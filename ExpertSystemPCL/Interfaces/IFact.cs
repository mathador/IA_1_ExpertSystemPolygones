namespace ExpertSystemPCL.Interfaces;

public interface IFact
{
    string Name();
    object Value();
    int Level();
    string Question();

    void SetLevel(int p);
}
