namespace AoC2023.Day9;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day9.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");
                List<List<int>> history = [];
                history.Add(inputString
                    .Split(' ')
                    .Select(int.Parse)
                    .ToList());
                while (history[^1].Any(h => h != 0))
                {
                    List<int> nextLayer = [];
                    for (int i = 1; i < history[^1].Count; i++)
                    {
                        nextLayer.Add(history[^1][i] - history[^1][i - 1]);
                    }
                    history.Add(nextLayer);
                }
                for (int i = 2; i <= history.Count; i++)
                {
                    history[^i].Add(history[^i][^1] + history[^(i - 1)][^1]);
                    history[^i].Insert(0, history[^i][0] - history[^(i - 1)][0]);
                }
                sum1 += history[0][^1];
                sum2 += history[0][0];
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods
}