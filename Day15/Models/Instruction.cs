namespace AoC2023.Day15.Models;

public class Instruction(string label, int hash, int? lensFocus = null)
{
    #region Public Properties

    public int Hash { get; set; } = hash;
    public string Label { get; set; } = label;

    public int? LensFocus { get; set; } = lensFocus;

    #endregion Public Properties
}