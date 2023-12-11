namespace AoC2023.Day10.Models;

public class Map : List<List<char>>
{
    #region Public Methods

    public char GetByPoint(Point point)
    {
        if (point.X < 0 || point.X >= Count
            || point.Y < 0 || point.Y >= this[0].Count)
        {
            return '*';
        }
        return this[point.X][point.Y];
    }

    public List<Point> GetLeft(Point point, Point previousPoint)
    {
        char currentChar = GetByPoint(point);
        if (currentChar == '|')
        {
            if (point.IsNorth(previousPoint))
            {
                return [point.GetEast()];
            }
            else if (point.IsSouth(previousPoint))
            {
                return [point.GetWest()];
            }
        }
        else if (currentChar == '-')
        {
            if (point.IsEast(previousPoint))
            {
                return [point.GetSouth()];
            }
            else if (point.IsWest(previousPoint))
            {
                return [point.GetNorth()];
            }
        }
        else if (currentChar == 'J')
        {
            if (point.IsNorth(previousPoint))
            {
                return [point.GetEast(), point.GetSouth()];
            }
            else if (point.IsWest(previousPoint))
            {
                return [];
            }
        }
        else if (currentChar == 'L')
        {
            if (point.IsNorth(previousPoint))
            {
                return [];
            }
            else if (point.IsEast(previousPoint))
            {
                return [point.GetSouth(), point.GetWest()];
            }
        }
        else if (currentChar == 'F')
        {
            if (point.IsSouth(previousPoint))
            {
                return [point.GetWest(), point.GetNorth()];
            }
            else if (point.IsEast(previousPoint))
            {
                return [];
            }
        }
        else if (currentChar == '7')
        {
            if (point.IsSouth(previousPoint))
            {
                return [];
            }
            else if (point.IsWest(previousPoint))
            {
                return [point.GetNorth(), point.GetEast()];
            }
        }

        throw new InvalidOperationException();
    }

    public Point GetNextPoint(Point point, Point previousPoint)
    {
        char currentChar = GetByPoint(point);
        if (currentChar == '|')
        {
            if (point.IsNorth(previousPoint))
            {
                return point.GetSouth();
            }
            else if (point.IsSouth(previousPoint))
            {
                return point.GetNorth();
            }
        }
        else if (currentChar == '-')
        {
            if (point.IsEast(previousPoint))
            {
                return point.GetWest();
            }
            else if (point.IsWest(previousPoint))
            {
                return point.GetEast();
            }
        }
        else if (currentChar == 'J')
        {
            if (point.IsNorth(previousPoint))
            {
                return point.GetWest();
            }
            else if (point.IsWest(previousPoint))
            {
                return point.GetNorth();
            }
        }
        else if (currentChar == 'L')
        {
            if (point.IsNorth(previousPoint))
            {
                return point.GetEast();
            }
            else if (point.IsEast(previousPoint))
            {
                return point.GetNorth();
            }
        }
        else if (currentChar == 'F')
        {
            if (point.IsSouth(previousPoint))
            {
                return point.GetEast();
            }
            else if (point.IsEast(previousPoint))
            {
                return point.GetSouth();
            }
        }
        else if (currentChar == '7')
        {
            if (point.IsSouth(previousPoint))
            {
                return point.GetWest();
            }
            else if (point.IsWest(previousPoint))
            {
                return point.GetSouth();
            }
        }

        throw new InvalidOperationException();
    }

    public List<Point> GetRight(Point point, Point previousPoint)
    {
        char currentChar = GetByPoint(point);
        if (currentChar == '|')
        {
            if (point.IsNorth(previousPoint))
            {
                return [point.GetWest()];
            }
            else if (point.IsSouth(previousPoint))
            {
                return [point.GetEast()];
            }
        }
        else if (currentChar == '-')
        {
            if (point.IsEast(previousPoint))
            {
                return [point.GetNorth()];
            }
            else if (point.IsWest(previousPoint))
            {
                return [point.GetSouth()];
            }
        }
        else if (currentChar == 'J')
        {
            if (point.IsNorth(previousPoint))
            {
                return [];
            }
            else if (point.IsWest(previousPoint))
            {
                return [point.GetSouth(), point.GetEast()];
            }
        }
        else if (currentChar == 'L')
        {
            if (point.IsNorth(previousPoint))
            {
                return [point.GetWest(), point.GetSouth()];
            }
            else if (point.IsEast(previousPoint))
            {
                return [];
            }
        }
        else if (currentChar == 'F')
        {
            if (point.IsSouth(previousPoint))
            {
                return [];
            }
            else if (point.IsEast(previousPoint))
            {
                return [point.GetNorth(), point.GetWest()];
            }
        }
        else if (currentChar == '7')
        {
            if (point.IsSouth(previousPoint))
            {
                return [point.GetNorth(), point.GetEast()];
            }
            else if (point.IsWest(previousPoint))
            {
                return [];
            }
        }

        throw new InvalidOperationException();
    }

    public List<Point> GetSecondPoints(Point start)
    {
        List<Point> result = [];
        var north = start.GetNorth();
        var south = start.GetSouth();
        var east = start.GetEast();
        var west = start.GetWest();

        if (CharCodes.FacingSouth.Contains(GetByPoint(north)))
        {
            result.Add(north);
        }
        if (CharCodes.FacingNorth.Contains(GetByPoint(south)))
        {
            result.Add(south);
        }
        if (CharCodes.FacingWest.Contains(GetByPoint(east)))
        {
            result.Add(east);
        }
        if (CharCodes.FacingEast.Contains(GetByPoint(west)))
        {
            result.Add(west);
        }

        if (result.Count != 2)
        {
            throw new Exception("Invalid input format");
        }

        return result;
    }

    #endregion Public Methods
}