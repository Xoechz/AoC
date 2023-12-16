namespace AoC2023.Day15.Models;

public class Lense(int value, int rank)
{
    #region Public Properties

    public int Rank { get; set; } = rank;
    public int Value { get; set; } = value;

    #endregion Public Properties
}