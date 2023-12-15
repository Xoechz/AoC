using AoC2023.Day14.Models;
using System.Drawing;

namespace AoC2023.Day14;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;
        Platform platform = new();

        var row = 0;
        var maxColumn = 0;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day14.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");
                var column = 0;
                foreach (var c in inputString)
                {
                    if (c != '.')
                    {
                        platform.Field.Add(new Point(column, row), c);
                    }

                    column++;
                    maxColumn = Math.Max(maxColumn, column);
                }
                row++;
            }
        }

        platform.MaxRow = row;
        platform.MaxColumn = maxColumn;

        platform.TiltNorth();
        sum1 = platform.GetValue();

        Dictionary<int, string> previousValues = [];
        var counter = 1;
        while (counter <= 1000000000)
        {
            platform.TiltNorth();
            platform.TiltWest();
            platform.TiltSouth();
            platform.TiltEast();

            var value = platform.ToString();
            var entries = previousValues.Where(e => e.Value == value);
            if (entries.Any()
                && (1000000000 - entries.Max(h => h.Key)) % (counter - entries.Max(h => h.Key)) == 0)
            {
                sum2 = platform.GetValue();
                break;
            }

            previousValues.Add(counter, value);
            counter++;
        }

        sum2 = platform.GetValue();

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods
}