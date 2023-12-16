using AoC2023.Day15.Models;

namespace AoC2023.Day15;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;
        List<Instruction> instructions = [];
        Dictionary<int, Dictionary<string, Lense>> boxes = [];

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day15.txt"))
        {
            var inputString = await file.ReadToEndAsync()
                 ?? throw new Exception("No string found");
            var strings = inputString.Split(',');
            foreach (var s in strings)
            {
                sum1 += GetHash(s);
                var parts = s.Replace("-", "").Split('=', StringSplitOptions.RemoveEmptyEntries);
                var label = parts[0];
                if (parts.Length == 2)
                {
                    instructions.Add(new(label, GetHash(label), int.Parse(parts[1])));
                }
                else
                {
                    instructions.Add(new(label, GetHash(label)));
                }
            }
        }
        for (int i = 0; i < 256; i++)
        {
            boxes.Add(i, []);
        }

        var rankCounter = 0;
        foreach (var instruction in instructions)
        {
            if (instruction.LensFocus.HasValue)
            {
                if (boxes[instruction.Hash].TryGetValue(instruction.Label, out var lense))
                {
                    lense.Value = instruction.LensFocus.Value;
                }
                else
                {
                    boxes[instruction.Hash].Add(instruction.Label, new(instruction.LensFocus.Value, rankCounter));
                }
            }
            else
            {
                boxes[instruction.Hash].Remove(instruction.Label);
            }
            rankCounter++;
        }
        foreach (var box in boxes)
        {
            var i = 1;
            foreach (var lenses in box.Value.OrderBy(l => l.Value.Rank))
            {
                sum2 += lenses.Value.Value * i * (box.Key + 1);
                i++;
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    private static int GetHash(string s)
    {
        var hash = 0;
        foreach (var c in s)
        {
            hash += c;
            hash = (hash * 17) % 256;
        }
        return hash;
    }

    #endregion Public Methods
}