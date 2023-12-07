using System.Text.RegularExpressions;

namespace AoC2023.Day1;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day1.txt"))
        {
            while (!file.EndOfStream)
            {
                var input = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                input = input.ToLower();
                sum1 += GetEncodedNumber(input);
                var task2 = ConvertNumbers(input);
                sum2 += GetEncodedNumber(task2);
            }
        }
        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods

    #region Private Methods

    private static string ConvertNumbers(string input) =>
        input.Replace("one", "one1one")
            .Replace("two", "two2two")
            .Replace("three", "three3three")
            .Replace("four", "four4four")
            .Replace("five", "five5five")
            .Replace("six", "six6six")
            .Replace("seven", "seven7seven")
            .Replace("eight", "eight8eight")
            .Replace("nine", "nine9nine");

    [GeneratedRegex("[0-9]")]
    private static partial Regex DigitRegex();

    private static int GetEncodedNumber(string input)
    {
        var matches = DigitRegex().Matches(input);
        if (matches.Count == 0)
        {
            throw new Exception("No digit in line found");
        }
        var firstDigit = int.Parse(matches.First().Value);
        var secondDigit = int.Parse(matches.Last().Value);
        return firstDigit * 10 + secondDigit;
    }

    #endregion Private Methods
}