using System.Text.RegularExpressions;

namespace AoC2023.Day3;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;
        List<string> schematic = [];
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day3.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                schematic.Add(inputString);
            }
        }

        //No number has 2 symbols as neighbors
        for (int i = 0; i < schematic.Count; i++)
        {
            var matches = Symbol().Matches(schematic[i]);
            foreach (var match in matches.ToList())
            {
                List<int> numbers = [];
                var lineLenght = schematic[i].Length;
                var symbolIndex = match.Index;

                if (i != 0)
                {
                    numbers.AddRange(Number().Matches(schematic[i - 1])
                        .Where(n => symbolIndex > n.Index - 2
                            && symbolIndex < n.Index + n.Length + 1)
                        .Select(n => int.Parse(n.Value)));
                }

                numbers.AddRange(Number().Matches(schematic[i])
                    .Where(n => symbolIndex > n.Index - 2
                        && symbolIndex < n.Index + n.Length + 1)
                    .Select(n => int.Parse(n.Value)));

                if (i != schematic.Count - 1)
                {
                    numbers.AddRange(Number().Matches(schematic[i + 1])
                        .Where(n => symbolIndex > n.Index - 2
                            && symbolIndex < n.Index + n.Length + 1)
                        .Select(n => int.Parse(n.Value)));
                }

                sum1 += numbers.Sum();
                if (match.Value == "*" && numbers.Count == 2)
                {
                    sum2 += numbers[0] * numbers[1];
                }
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods

    #region Private Methods

    [GeneratedRegex("[0-9]+")]
    private static partial Regex Number();

    [GeneratedRegex("[^0-9.]")]
    private static partial Regex Symbol();

    #endregion Private Methods
}