using FormesGeometriques;

namespace TuParser;

public sealed class UtRuleParser
{
    public static IEnumerable<object[]> RuleParsingTestData()
    {
        yield return new object[]
        {
            "R1 : IF (Ordre=3(Quel est l'ordre ?)) THEN Triangle",
            new Rule() {
                Name = "R1",
                Premisses =[new IntFait(3) {
                    Nom = "Ordre",
                    Question = "Quel est l'ordre ?"
                }],
                Conclusion = "Triangle" }
        };
        yield return new object[]
        {
            "R8 : IF (Quadrilatère AND Cotes Paralleles=4(Combien y'a-t-il de côtés parallèles entre eux - 0, 2 ou 4)) THEN Parallélogramme",
            new Rule() {
                Name = "R8",
                Premisses = [
                    new Fait { Nom = "Quadrilatère" },
                    new IntFait(4) {
                        Nom="Cotes Paralleles",
                        Question = "Combien y'a-t-il de côtés parallèles entre eux - 0, 2 ou 4"
                    }],
                Conclusion = "Parallélogramme" }
        };
    }

    [Theory]
    [MemberData(nameof(RuleParsingTestData))]
    public void RuleParsing_IsCorrect(string rule, Rule parsedRule)
    {
        // Arrange
        // Fait dans RulesParsingTestData

        // Act
        var result = RuleParser.Parse(rule);

        // Assert
        Assert.Equivalent(parsedRule, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RuleParsing_ThrowsArgumentException_WhenInputIsNull(string rule)
    {
        // Arrange
        // Already done in inline data

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RuleParser.Parse(rule));
    }

    [Fact]
    public void RuleParsing_ThrowsArgumentException_WhenRuleDoesNotContainSeparator()
    {
        // Arrange
        var rule = "R1 IF (Ordre=3(Quel est l'ordre ?)) THEN Triangle";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RuleParser.Parse(rule));
    }

    [Fact]
    public void RuleParsing_ThrowsArgumentException_WhenRuleDoesNotContainCondition()
    {
        // Arrange
        var rule = "R1 : THEN Triangle";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RuleParser.Parse(rule));
    }

    [Fact]
    public void RuleParsing_ThrowsArgumentException_WhenRuleDoesNotContainConclusion()
    {
        // Arrange
        var rule = "R1 : IF (Ordre=3(Quel est l'ordre ?))";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => RuleParser.Parse(rule));
    }
}