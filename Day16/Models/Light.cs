using System.Drawing;

namespace AoC2023.Day16.Models;

public class Light(int x, int y, Direction direction)
{
    #region Public Properties

    public Direction Direction { get; set; } = direction;
    public List<Point> HeatedPoints { get; set; } = [];
    public List<Light> NextLights { get; set; } = [];
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    #endregion Public Properties

    #region Public Methods

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var light = obj as Light;
        return X == light?.X
            && Y == light?.Y
            && Direction == light?.Direction;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Direction.GetHashCode();
    }

    #endregion Public Methods
}