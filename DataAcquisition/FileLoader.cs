namespace DataAcquisition;
public static class FileLoader
{
    public static string[] LoadFile(string filePath)
    {
        var arrayEmpty = Array.Empty<string>();
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return arrayEmpty;
        }
        if (!File.Exists(filePath))
        {
            return arrayEmpty;
        }

        return File.ReadAllLines(filePath);
    }
}
