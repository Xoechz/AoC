using AoC2023.Day12.Models;

namespace AoC2023.Day12;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        long sum1 = 0;
        long sum2 = 0;

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day12.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");
                var parts = inputString.Split(' ');
                var spring1 = new Spring(parts[0],
                    parts[1].Split(',')
                        .Select(int.Parse)
                        .ToList());

                var spring2String = spring1.Id + "?" + spring1.Id + "?" + spring1.Id + "?" + spring1.Id + "?" + spring1.Id;
                var spring2Counts = spring1.Counts.ToList();
                spring2Counts.AddRange(spring1.Counts);
                spring2Counts.AddRange(spring1.Counts);
                spring2Counts.AddRange(spring1.Counts);
                spring2Counts.AddRange(spring1.Counts);

                var spring2 = new Spring(spring2String, spring2Counts);
                sum1 += GetNumberOfAllPossibleOptions(spring1);
                sum2 += GetNumberOfAllPossibleOptions(spring2);
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    private static long GetNumberOfAllPossibleOptions(Spring spring)
    {
        List<Spring> options = [spring];

        while (options.Any(d => d.Id.Contains('?')))
        {
            List<Spring> next = options.Where(d => !d.Id.Contains('?'))
                .ToList();
            foreach (var option in options.Where(d => d.Id.Contains('?')))
            {
                var currentString = option.Id;
                var index = currentString.IndexOf('?');
                var nextSpring1 = new Spring(currentString.Remove(index, 1).Insert(index, "."), option.Counts, option.Value);
                var nextSpring2 = new Spring(currentString.Remove(index, 1).Insert(index, "#"), option.Counts, option.Value);
                if (nextSpring1.IsValid())
                {
                    nextSpring1.Shorten();
                    next.Add(nextSpring1);
                }
                if (nextSpring2.IsValid())
                {
                    nextSpring2.Shorten();
                    next.Add(nextSpring2);
                }
            }

            options = next
                .GroupBy(g => g.Id + " " + g.Counts.Count)
                .Select(g => new Spring(g.First().Id, g.First().Counts, g.Sum(s => s.Value)))
                .ToList();
        }

        return options.Where(s => s.IsValid())
            .Sum(s => s.Value);
    }

    #endregion Public Methods
}