using AoC2023.Day2.Models;
using System.Collections.ObjectModel;

namespace AoC2023.Day2;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var maxDict = new ReadOnlyDictionary<Color, int>(new Dictionary<Color, int>()
        {
            { Color.Red, MAX_RED },
            { Color.Green, MAX_GREEN },
            { Color.Blue, MAX_BLUE },
        });
        var sum1 = 0;
        var sum2 = 0;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day2.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                var game = MapToGame(inputString);
                if (IsGamePossible(game, maxDict))
                {
                    sum1 += game.Id;
                }
                sum2 += GetGamePower(game);
            }
        }
        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods

    #region Private Fields

    private const int MAX_BLUE = 14;
    private const int MAX_GREEN = 13;
    private const int MAX_RED = 12;

    #endregion Private Fields

    #region Private Methods

    private static int GetGamePower(Game game)
    {
        var power = 1;
        foreach (var color in Enum.GetValues(typeof(Color)).Cast<Color>())
        {
            power *= game.Sets.Max(s => s.Counts.TryGetValue(color, out var count) ? count : 0);
        }
        return power;
    }

    private static bool IsGamePossible(Game game, IReadOnlyDictionary<Color, int> maxDict)
            => !game.Sets.Any(s => s.Counts.Any(c => c.Value > maxDict.GetValueOrDefault(c.Key)));

    private static Game MapToGame(string inputString)
    {
        var parts = inputString.Split(':');
        if (!inputString.StartsWith("Game ") || parts.Length != 2)
        {
            throw new Exception("Invalid game format");
        }
        var gameInfo = parts[0];
        var id = int.Parse(gameInfo[5..]);

        var setStrings = parts[1].Split(';');
        List<Set> sets = [];
        foreach (var setString in setStrings)
        {
            sets.Add(MapToSet(setString));
        }

        return new Game { Id = id, Sets = sets };
    }

    private static Set MapToSet(string inputString)
    {
        var dict = new Dictionary<Color, int>();
        var types = inputString.Split(',', StringSplitOptions.TrimEntries);

        foreach (var type in types)
        {
            var parts = type.Split(' ', StringSplitOptions.TrimEntries);
            if (parts.Length != 2)
            {
                throw new Exception("Invalid set format");
            }
            var count = int.Parse(parts[0]);
            var color = parts[1];
            if (Enum.TryParse(typeof(Color), color, true, out var colorEnum))
            {
                dict.Add((Color)colorEnum, count);
            }
            else
            {
                throw new Exception("Invalid color value");
            }
        }

        return new Set { Counts = dict };
    }

    #endregion Private Methods
}