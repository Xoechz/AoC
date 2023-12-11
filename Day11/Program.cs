using AoC2023.Day11.Models;

namespace AoC2023.Day11;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        long sum2 = 0;
        List<List<char>> universe = [];
        List<Galaxy> galaxies1 = [];
        List<Galaxy> galaxies2 = [];
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day11.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                universe.Add([.. inputString]);
            }
        }

        for (var x = 0; x < universe.Count; x++)
        {
            for (var y = 0; y < universe[x].Count; y++)
            {
                if (universe[x][y] == '#')
                {
                    galaxies1.Add(new Galaxy(x, y));
                    galaxies2.Add(new Galaxy(x, y));
                }
            }
        }

        var emptyLines = Enumerable.Range(0, universe.Count)
            .Where(x => !galaxies1.Any(g => g.X == x))
            .OrderDescending();

        var emptyColumns = Enumerable.Range(0, universe[0].Count)
            .Where(y => !galaxies1.Any(g => g.Y == y))
            .OrderDescending();

        foreach (var emptyLine in emptyLines)
        {
            foreach (var galaxy1 in galaxies1.Where(g => g.X > emptyLine))
            {
                galaxy1.X++;
            }
            foreach (var galaxy2 in galaxies2.Where(g => g.X > emptyLine))
            {
                galaxy2.X += 999999;
            }
        }

        foreach (var emptyColumn in emptyColumns)
        {
            foreach (var galaxy1 in galaxies1.Where(g => g.Y > emptyColumn))
            {
                galaxy1.Y++;
            }
            foreach (var galaxy2 in galaxies2.Where(g => g.Y > emptyColumn))
            {
                galaxy2.Y += 999999;
            }
        }

        for (var i = 0; i < galaxies1.Count; i++)
        {
            for (var j = i + 1; j < galaxies1.Count; j++)
            {
                sum1 += Math.Abs(galaxies1[i].X - galaxies1[j].X)
                    + Math.Abs(galaxies1[i].Y - galaxies1[j].Y);
            }
        }

        for (var i = 0; i < galaxies2.Count; i++)
        {
            for (var j = i + 1; j < galaxies2.Count; j++)
            {
                sum2 += Math.Abs(galaxies2[i].X - galaxies2[j].X)
                    + Math.Abs(galaxies2[i].Y - galaxies2[j].Y);
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods
}