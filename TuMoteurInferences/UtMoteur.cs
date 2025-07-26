using DataAcquisition;
using FormesGeometriques;

namespace TuMoteurInferences;

public class UtMoteur
{
    const string FILE_NAME = "rules.rls";
    private readonly MoteurDInferences _moteur;

    public UtMoteur()
    {
        // chargement des règles depuis la base de données
        var regles = FileLoader.LoadFile(FILE_NAME);

        // initialisation du moteur de règles
        _moteur = new MoteurDInferences(regles);
    }

    [Fact]
    public void Moteur_SiFaitNull_ReturnUnFaitAvecQuestion()
    {
        // Arrange

        // Act
        var premiereQuestion = _moteur.GetRulesFor(new() { Nom = "" })
            .FirstOrDefault();

        // Assert
        Assert.NotNull(premiereQuestion);
        //Assert.NotEmpty(premiereQuestion.Premisses);
        Assert.Contains(premiereQuestion.Premisses, p => !string.IsNullOrWhiteSpace(p.Question));
    }

    public static IEnumerable<object[]> GetFaitEtConclusion()
    {
        yield return new object[] { new IntFait(3) { Nom = "Ordre" }, new Rule { Conclusion = "Triangle" } };
        yield return new object[] { new IntFait(4) { Nom = "Ordre" }, new Rule { Conclusion = "Quadrilatère" } };
        yield return new object[] { new BoolFait() { Nom = "Angle Droit", Valeur = true }, new Rule { Conclusion = "Triangle Rectangle" } };
        yield return new object[] { new BoolFait() { Nom = "Angle Droit", Valeur = false }, null! };
    }

    [Theory]
    [MemberData(nameof(GetFaitEtConclusion))]
    public void Moteur_SiFait_ReturnUneConclusionAdaptée(Fait f, Rule conclusionAttendue)
    {
        // Arrange

        // Act
        var rule = _moteur.GetRulesFor(f)
            .FirstOrDefault();

        // Assert
        Assert.Equal(conclusionAttendue?.Conclusion, rule?.Conclusion);
    }
}