using AoC2023.Day16.Models;
using System.Drawing;

namespace AoC2023.Day16;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        int sum1;
        int sum2;
        LightMap map = new();

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day16.txt"))
        {
            var row = 0;
            var maxColum = 0;
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");
                var column = 0;
                foreach (var c in inputString)
                {
                    if (c != '.')
                    {
                        map.Map.Add(new Point(column, row), c);
                    }
                    maxColum = Math.Max(maxColum, column);
                    column++;
                }
                row++;
            }
            map.MaxRow = row - 1;
            map.MaxColumn = maxColum;
        }

        var initialLight = new Light(-1, 0, Direction.East);

        sum1 = map.CalculateHeatedPoints(initialLight);
        sum2 = sum1;

        for (var i = 0; i <= map.MaxColumn; i++)
        {
            initialLight = new Light(i, map.MaxRow + 1, Direction.North);
            var heatedPointCount = map.CalculateHeatedPoints(initialLight);
            sum2 = Math.Max(sum2, heatedPointCount);
            Console.WriteLine("North " + i + " " + heatedPointCount);
        }
        for (var i = 0; i <= map.MaxRow; i++)
        {
            initialLight = new Light(-1, i, Direction.East);
            var heatedPointCount = map.CalculateHeatedPoints(initialLight);
            sum2 = Math.Max(sum2, heatedPointCount);
            Console.WriteLine("East " + i + " " + heatedPointCount);
        }
        for (var i = 0; i <= map.MaxColumn; i++)
        {
            initialLight = new Light(i, -1, Direction.South);
            var heatedPointCount = map.CalculateHeatedPoints(initialLight);
            sum2 = Math.Max(sum2, heatedPointCount);
            Console.WriteLine("South " + i + " " + heatedPointCount);
        }
        for (var i = 0; i <= map.MaxRow; i++)
        {
            initialLight = new Light(map.MaxColumn + 1, i, Direction.West);
            var heatedPointCount = map.CalculateHeatedPoints(initialLight);
            sum2 = Math.Max(sum2, heatedPointCount);
            Console.WriteLine("West " + i + " " + heatedPointCount);
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    #endregion Public Methods
}