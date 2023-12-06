namespace AoC2023.Day6;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var prod1 = 1;
        var counter2 = 0;
        var races = new Dictionary<int, int>();
        long time, distance;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day6.txt"))
        {
            var inputString = await file.ReadLineAsync()
                ?? throw new Exception("No string found");
            var times = inputString[5..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            time = long.Parse(inputString[5..].Replace(" ", ""));

            inputString = await file.ReadLineAsync()
                ?? throw new Exception("No string found");
            var distances = inputString[9..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            distance = long.Parse(inputString[9..].Replace(" ", ""));

            if (distances.Length != times.Length)
            {
                throw new Exception("Invalid input file format");
            }
            for (var i = 0; i < times.Length; i++)
            {
                races.Add(int.Parse(times[i]), int.Parse(distances[i]));
            }
        }

        foreach (var race in races)
        {
            var counter = 0;
            for (var i = 1; i < race.Key; i++)
            {
                if (i * (race.Key - i) > race.Value)
                {
                    counter++;
                }
            }
            prod1 *= counter;
        }

        for (long i = 1; i < time; i++)
        {
            if (i * (time - i) > distance)
            {
                counter2++;
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(prod1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(counter2);
    }
}