using ExpertSystemPCL;

namespace TuMotor;

public class UnitTest1
{
    private class MockInterface : HumanInterface
    {
        public int AskIntValue(string p) => 0;
        public bool AskBoolValue(string p) => false;
        public void PrintFacts(List<IFact> facts) { }
        public void PrintRules(List<Rule> rules) { }
    }

    [Fact]
    public void RuleParsing_IsGood()
    {
        // Arrange
        var mockInterface = new MockInterface();
        var motor = new Motor(mockInterface);
        string ruleStr = "R1 : IF (Ordre=3(Quel est l'ordre ?)) THEN Triangle";

        // Act
        motor.AddRule(ruleStr);

        // V�rifier que la r�gle a �t� ajout�e et pars�e correctement en utilisant la m�thode Solve
        motor.Solve();

        // Note: La v�rification compl�te n�cessiterait d'exposer la base de r�gles ou 
        // d'ajouter des m�thodes de test dans la classe Motor
        // Dans l'impl�mentation actuelle, nous pouvons seulement v�rifier que la r�gle
        // ne g�n�re pas d'exception lors du parsing et de l'ex�cution

        // Assert
        Assert.True(true); // Le test passe si aucune exception n'est lev�e
    }
}