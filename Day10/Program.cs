using AoC2023.Day10.Models;

namespace AoC2023.Day10;

public partial class Program
{
    #region Public Methods
    // THIS IS SHIT; DO NOT DO IT THIS WAY
    public static async Task Main(string[] args)
    {
        var steps1 = 1;
        var sum2 = 0;
        Map map = [];
        Point start = new(-1, -1);
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day10.txt"))
        {
            var i = 0;
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");
                if (inputString.Contains('S'))
                {
                    start = new(i, inputString.IndexOf('S'));
                }

                i++;
                map.Add([.. inputString]);
            }
        }
        var previousPoint1 = start;
        var previousPoint2 = start;
        var points = map.GetSecondPoints(start);
        var point1 = points[0];
        var point2 = points[1];
        var endFound = false;
        var path1 = new List<Point>() { start, point1 };
        var path2 = new List<Point>() { start, point2 };
        while (!endFound)
        {
            var nextPoint1 = map.GetNextPoint(point1, previousPoint1);
            previousPoint1 = point1;
            point1 = nextPoint1;
            var nextPoint2 = map.GetNextPoint(point2, previousPoint2);
            previousPoint2 = point2;
            point2 = nextPoint2;

            path1.Add(point1);
            path2.Add(point2);

            if (point1.Equals(point2))
            {
                endFound = true;
            }

            steps1++;
        }

        for (var x = 0; x < map.Count; x++)
        {
            for (var y = 0; y < map[x].Count; y++)
            {
                if (!path1.Any(p => p.X == x && p.Y == y)
                    && !path2.Any(p => p.X == x && p.Y == y))
                {
                    map[x][y] = ' ';
                }
            }
        }

        bool canBe2 = true;
        bool canBe1 = true;

        for (var i = 1; i < path1.Count; i++)
        {
            var basePoint = path1[i];
            var rights = map.GetRight(path1[i], path1[i - 1]);
            var lefts = map.GetLeft(path1[i], path1[i - 1]);
            for (var j = 0; j < rights.Count; j++)
            {
                while (map.GetByPoint(rights[j]) == ' '
                    || map.GetByPoint(rights[j]) == '1')
                {
                    map[rights[j].X][rights[j].Y] = '1';
                    var nextRight = rights[j].GetInFront(basePoint);
                    basePoint = rights[j];
                    rights[j] = nextRight;
                    if (map.GetByPoint(rights[j]) == '*')
                    {
                        canBe1 = false;
                    }
                }
                basePoint = path1[i];
            }

            for (var j = 0; j < lefts.Count; j++)
            {
                while (map.GetByPoint(lefts[j]) == ' '
                    || map.GetByPoint(lefts[j]) == '2')
                {
                    map[lefts[j].X][lefts[j].Y] = '2';
                    var nextLeft = lefts[j].GetInFront(basePoint);
                    basePoint = lefts[j];
                    lefts[j] = nextLeft;
                    if (map.GetByPoint(lefts[j]) == '*')
                    {
                        canBe2 = false;
                    }
                }
                basePoint = path1[i];
            }
        }

        for (var i = 1; i < path2.Count; i++)
        {
            var basePoint = path2[i];
            var rights = map.GetRight(path2[i], path2[i - 1]);
            var lefts = map.GetLeft(path2[i], path2[i - 1]);
            for (var j = 0; j < rights.Count; j++)
            {
                while (map.GetByPoint(rights[j]) == ' '
                    || map.GetByPoint(rights[j]) == '2')
                {
                    map[rights[j].X][rights[j].Y] = '2';
                    var nextRight = rights[j].GetInFront(basePoint);
                    basePoint = rights[j];
                    rights[j] = nextRight;
                    if (map.GetByPoint(rights[j]) == '*')
                    {
                        canBe2 = false;
                    }
                }
                basePoint = path2[i];
            }

            for (var j = 0; j < lefts.Count; j++)
            {
                while (map.GetByPoint(lefts[j]) == ' '
                    || map.GetByPoint(lefts[j]) == '1')
                {
                    map[lefts[j].X][lefts[j].Y] = '1';
                    var nextLeft = lefts[j].GetInFront(basePoint);
                    basePoint = lefts[j];
                    lefts[j] = nextLeft;
                    if (map.GetByPoint(lefts[j]) == '*')
                    {
                        canBe1 = false;
                    }
                }
                basePoint = path2[i];
            }
        }

        foreach (var line in map)
        {
            foreach (char c in line)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }
        var rightCount = map.Sum(l => l.Count(c => c == '1'));
        var leftCount = map.Sum(l => l.Count(c => c == '2'));
        if (!canBe2 && canBe1)
        {
            sum2 = rightCount;
        }
        else if (canBe2 && !canBe1)
        {
            sum2 = leftCount;
        }
        else
        {
            if (rightCount < leftCount)
            {
                sum2 = rightCount;
            }
            else
            {
                sum2 = leftCount;
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(steps1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    //| - L J 7 F

    #endregion Public Methods
}