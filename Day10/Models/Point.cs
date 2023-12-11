namespace AoC2023.Day10.Models;

public class Point(int x, int y)
{
    #region Public Properties

    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    #endregion Public Properties

    #region Public Methods

    public override bool Equals(object? obj) =>
        obj is Point point
            && X == point.X
            && Y == point.Y;

    public Point GetEast() => new(X, Y + 1);

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public Point GetInFront(Point previousPoint)
    {
        if (IsNorth(previousPoint))
        {
            return GetSouth();
        }
        if (IsEast(previousPoint))
        {
            return GetWest();
        }
        if (IsSouth(previousPoint))
        {
            return GetNorth();
        }
        if (IsWest(previousPoint))
        {
            return GetEast();
        }
        throw new InvalidOperationException();
    }

    public Point GetNorth() => new(X - 1, Y);

    public Point GetSouth() => new(X + 1, Y);

    public Point GetWest() => new(X, Y - 1);

    public bool IsEast(Point point) =>
        X == point.X && Y + 1 == point.Y;

    public bool IsNorth(Point point) =>
            X - 1 == point.X && Y == point.Y;

    public bool IsSouth(Point point) =>
        X + 1 == point.X && Y == point.Y;

    public bool IsWest(Point point) =>
        X == point.X && Y - 1 == point.Y;

    #endregion Public Methods
}