namespace TuDataAcquisition;

public class UtDataAcquisition
{
    [Fact]
    public void CheckFile_ReturnsLinesNotEmpties()
    {
        // Arrange
        var filePath = "rules.rls";

        // Act
        var result = DataAcquisition.FileLoader.LoadFile(filePath);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, line => Assert.False(string.IsNullOrWhiteSpace(line)));
    }
}