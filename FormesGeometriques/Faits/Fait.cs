namespace FormesGeometriques;
public class Fait
{
    /// <summary>
    /// Nom du fait
    /// </summary>
    public string Nom { get; set; }
    /// <summary>
    /// 0 based level of the fact in the hierarchy.
    /// </summary>
    public uint Level { get; set; } = 0;
    /// <summary>
    /// Question
    /// </summary>
    public string Question { get; set; } = string.Empty;
}
