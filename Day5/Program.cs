using AoC2023.Day5.Models;

namespace AoC2023.Day5;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        long min1 = 0;
        long min2 = 0;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day5.txt"))
        {
            List<long> seeds1 = [];
            List<SeedRange> seeds2 = [];
            List<Map> maps = [];

            var inputString = await file.ReadLineAsync()
                ?? throw new Exception("No string found");

            var seedStrings = inputString[7..].Split(' ');
            var even = false;
            foreach (var seedString in seedStrings)
            {
                var seed = long.Parse(seedString);
                seeds1.Add(seed);
                if (even)
                {
                    seeds2.Last().Range = seed;
                }
                else
                {
                    seeds2.Add(new SeedRange { Start = seed });
                }
                even = !even;
            }

            while (!file.EndOfStream)
            {
                inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");

                if (inputString.Contains("map"))
                {
                    continue;
                }

                var mapStrings = inputString.Split(' ');
                if (mapStrings.Length != 3 && maps.Count != 0)
                {
                    seeds1 = seeds1.Select(s => ApplyMap(maps, s))
                        .ToList();
                    seeds2 = ApplyMapToSeedRanges(maps, seeds2);

                    maps.Clear();
                }
                else if (mapStrings.Length == 3)
                {
                    maps.Add(new Map(mapStrings));
                }
            }

            if (maps.Count != 0)
            {
                seeds1 = seeds1.Select(s => ApplyMap(maps, s))
                    .ToList();
                seeds2 = ApplyMapToSeedRanges(maps, seeds2);

                maps.Clear();
            }

            min1 = seeds1.Min();
            min2 = seeds2.Min(s => s.Start);
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(min1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(min2);
    }

    #endregion Public Methods

    #region Private Methods

    private static long ApplyMap(List<Map> maps, long input)
    {
        var map = maps.Find(m => m.Source <= input
            && m.Source + m.Range > input);

        if (map == null)
        {
            return input;
        }

        return input - map.Source + map.Destination;
    }

    private static List<SeedRange> ApplyMapToSeedRanges(List<Map> maps, List<SeedRange> seedRanges)
    {
        List<SeedRange> result = [];
        seedRanges = SplitSeedRangeList(maps, seedRanges);

        foreach (var seedRange in seedRanges)
        {
            var map = maps.Find(m => m.Source <= seedRange.Start
                && m.Source + m.Range > seedRange.Start);

            if (map == null)
            {
                result.Add(seedRange);
            }
            else
            {
                seedRange.Start = seedRange.Start - map.Source + map.Destination;
                result.Add(seedRange);
            }
        }

        return result;
    }

    private static List<SeedRange> SplitSeedRange(List<Map> maps, SeedRange seedRange)
    {
        List<SeedRange> result = [];

        var map = maps.Find(m => m.Source <= seedRange.Start
            && m.Source + m.Range > seedRange.Start);

        long nextValue;
        if (map == null)
        {
            var nextMaps = maps.Where(m => m.Source > seedRange.Start);
            if (nextMaps.Any())
            {
                nextValue = nextMaps.Min(m => m.Source);
            }
            else
            {
                nextValue = long.MaxValue;
            }
        }
        else
        {
            nextValue = map.Source + map.Range;
        }

        if (nextValue < seedRange.Start + seedRange.Range)
        {
            var newRange = nextValue - seedRange.Start;

            result.Add(new SeedRange
            {
                Start = seedRange.Start,
                Range = newRange
            });

            result.AddRange(SplitSeedRange(maps, new SeedRange
            {
                Start = nextValue,
                Range = seedRange.Range - newRange
            }));
        }
        else
        {
            result.Add(seedRange);
        }

        return result;
    }

    private static List<SeedRange> SplitSeedRangeList(List<Map> maps, List<SeedRange> seedRanges)
    {
        List<SeedRange> result = [];

        foreach (var seedRange in seedRanges)
        {
            result.AddRange(SplitSeedRange(maps, seedRange));
        }

        return result;
    }

    #endregion Private Methods
}