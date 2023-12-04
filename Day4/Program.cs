namespace AoC2023.Day4;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;
        var card = 0;
        var duplicateDict = new Dictionary<int, int>();
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day4.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                var parts = inputString.Split('|');
                if (parts.Length != 2)
                {
                    throw new Exception("Invalid Card Format.");
                }
                var yourNumbers = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var winningNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var scoringNumbers = yourNumbers.Where(n => winningNumbers.Contains(n))
                    .Select(int.Parse)
                    .ToList();

                if (scoringNumbers.Count != 0)
                {
                    sum1 += (int)Math.Pow(2, scoringNumbers.Count - 1);
                }

                var cardCount = duplicateDict.TryGetValue(card, out var duplication) ? duplication + 1 : 1;
                sum2 += cardCount;

                for (var i = 1; i <= scoringNumbers.Count; i++)
                {
                    if (duplicateDict.ContainsKey(card + i))
                    {
                        duplicateDict[card + i] += cardCount;
                    }
                    else
                    {
                        duplicateDict[card + i] = cardCount;
                    }
                }

                card++;
            }
        }
        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }
}