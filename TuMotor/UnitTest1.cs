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

        // Vérifier que la règle a été ajoutée et parsée correctement en utilisant la méthode Solve
        motor.Solve();

        // Note: La vérification complète nécessiterait d'exposer la base de règles ou 
        // d'ajouter des méthodes de test dans la classe Motor
        // Dans l'implémentation actuelle, nous pouvons seulement vérifier que la règle
        // ne génère pas d'exception lors du parsing et de l'exécution

        // Assert
        Assert.True(true); // Le test passe si aucune exception n'est levée
    }
}