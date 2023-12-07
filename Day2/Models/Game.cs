namespace AoC2023.Day2.Models;

public class Game
{
    #region Public Properties

    public required int Id { get; set; }
    public required IEnumerable<Set> Sets { get; set; }

    #endregion Public Properties
}