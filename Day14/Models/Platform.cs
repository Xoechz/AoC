using System.Drawing;
using System.Text;

namespace AoC2023.Day14.Models;

public class Platform
{
    #region Public Properties

    public Dictionary<Point, char> Field { get; set; } = [];
    public int MaxColumn { get; set; }

    public int MaxRow { get; set; }

    #endregion Public Properties

    #region Public Methods

    public int GetValue() => Field.Sum(p => (MaxRow - p.Key.Y) * (p.Value == 'O' ? 1 : 0));

    public void TiltEast()
    {
        var roundRocks = Field.Where(p => p.Value == 'O'
            && p.Key.X != MaxColumn - 1
            && (!Field.TryGetValue(new Point(p.Key.X + 1, p.Key.Y), out var c)
                || c != '#'))
        .OrderByDescending(p => p.Key.X)
        .ToList();

        foreach (var item in roundRocks)
        {
            var row = Field.Where(p => p.Key.Y == item.Key.Y
                && p.Key.X > item.Key.X);

            var newX = row.Any()
                ? row.Min(c => c.Key.X) - 1
                : MaxColumn - 1;

            if (newX != item.Key.X)
            {
                Field.Remove(item.Key);
                Field.Add(new Point(newX, item.Key.Y), item.Value);
            }
        }
    }

    public void TiltNorth()
    {
        var roundRocks = Field.Where(p => p.Value == 'O'
                && p.Key.Y != 0
                && (!Field.TryGetValue(new Point(p.Key.X, p.Key.Y - 1), out var c)
                    || c != '#'))
            .OrderBy(p => p.Key.Y)
            .ToList();

        foreach (var item in roundRocks)
        {
            var column = Field.Where(p => p.Key.X == item.Key.X
                && p.Key.Y < item.Key.Y);

            var newY = column.Any()
                ? column.Max(c => c.Key.Y) + 1
                : 0;

            if (newY != item.Key.Y)
            {
                Field.Remove(item.Key);
                Field.Add(new Point(item.Key.X, newY), item.Value);
            }
        }
    }

    public void TiltSouth()
    {
        var roundRocks = Field.Where(p => p.Value == 'O'
            && p.Key.Y != MaxRow - 1
            && (!Field.TryGetValue(new Point(p.Key.X, p.Key.Y + 1), out var c)
                || c != '#'))
        .OrderByDescending(p => p.Key.Y)
        .ToList();

        foreach (var item in roundRocks)
        {
            var column = Field.Where(p => p.Key.X == item.Key.X
                && p.Key.Y > item.Key.Y);

            var newY = column.Any()
                ? column.Min(c => c.Key.Y) - 1
                : MaxRow - 1;

            if (newY != item.Key.Y)
            {
                Field.Remove(item.Key);
                Field.Add(new Point(item.Key.X, newY), item.Value);
            }
        }
    }

    public void TiltWest()
    {
        var roundRocks = Field.Where(p => p.Value == 'O'
            && p.Key.X != 0
            && (!Field.TryGetValue(new Point(p.Key.X - 1, p.Key.Y), out var c)
                || c != '#'))
        .OrderBy(p => p.Key.X)
        .ToList();

        foreach (var item in roundRocks)
        {
            var row = Field.Where(p => p.Key.Y == item.Key.Y
                && p.Key.X < item.Key.X);

            var newX = row.Any()
                ? row.Max(c => c.Key.X) + 1
                : 0;

            if (newX != item.Key.X)
            {
                Field.Remove(item.Key);
                Field.Add(new Point(newX, item.Key.Y), item.Value);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder result = new();
        for (int y = 0; y < MaxRow; y++)
        {
            for (int x = 0; x < MaxColumn; x++)
            {
                if (Field.TryGetValue(new Point(x, y), out var c))
                {
                    result.Append(c);
                }
                else
                {
                    result.Append(' ');
                }
            }
            result.AppendLine();
        }
        return result.ToString();
    }

    #endregion Public Methods
}