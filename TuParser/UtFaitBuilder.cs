using FormesGeometriques;

namespace TuParser;
public class UtFaitBuilder
{
    public static IEnumerable<object[]> FaitParsingTestData()
    {
        yield return new object[]
        {
            "Triangle",
            new Fait { Nom = "Triangle" }
        };
        yield return new object[]
        {
            "Ordre=3(Quel est l'ordre ?))",
            new IntFait(3) { Nom = "Ordre", Question="Quel est l'ordre ?" }
        };
        yield return new object[]
        {
            "Angle Droit(La figure a-t-elle au moins un angle droit ?)",
            new BoolFait { Nom = "Angle Droit", Question="La figure a-t-elle au moins un angle droit ?" }
        };
    }

    [Theory]
    [MemberData(nameof(FaitParsingTestData))]
    public void FaitParsing_IsCorrect(string faitString, Fait expectedFait)
    {
        // Arrange
        var separatedPremisses = faitString
                                .Replace(")", string.Empty)
                                .Split(["(", "=", ":"], StringSplitOptions.RemoveEmptyEntries);

        // Act
        var result = FaitHelpers.BuildFait(separatedPremisses);

        // Assert
        Assert.Equivalent(expectedFait, result);
    }

    [Theory]
    [InlineData(null)]
    //[InlineData(Array.Empty<string>())]
    public void FaitParsing_ThrowsArgumentException_WhenInputIsNull(string[] separated)
    {
        // Arrange
        // Already done in inline data

        // Act & Assert
        Assert.Throws<ArgumentException>(() => FaitHelpers.BuildFait(separated));
    }

    [Fact]
    public void FaitParsing_ThrowsArgumentException_WhenInputIsEmpty()
    {
        // Arrange
        string[] separated = Array.Empty<string>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => FaitHelpers.BuildFait(separated));
    }

    [Fact]
    public void FaitParsing_ThrowsArgumentException_WhenInputHasTooManyParts()
    {
        // Arrange
        string[] separated = ["Fait", "Part1", "Part2", "Part3", "Part4"];
        // Act & Assert
        Assert.Throws<ArgumentException>(() => FaitHelpers.BuildFait(separated));
    }
}
