namespace AoC2023.Day13;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var sum1 = 0;
        var sum2 = 0;

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day13.txt"))
        {
            while (!file.EndOfStream)
            {
                Dictionary<int, int> rowValues = [];
                Dictionary<int, int> columnValues = [];
                List<int> possibleRows = [];
                List<int> possibleColumns = [];

                var inputString = await file.ReadLineAsync()
                     ?? throw new Exception("No string found");

                var rowBinary = 1;
                var rowCounter = 0;
                while (!string.IsNullOrWhiteSpace(inputString) && !file.EndOfStream)
                {
                    var rowValue = 0;
                    var columnCounter = 0;
                    var columnBinary = 1;

                    possibleRows.Add(rowCounter);

                    foreach (var c in inputString)
                    {
                        if (rowBinary == 1)
                        {
                            possibleColumns.Add(columnCounter);
                            columnValues.Add(columnCounter, 0);
                        }

                        if (c == '#')
                        {
                            rowValue += columnBinary;
                            columnValues[columnCounter] += rowBinary;
                        }

                        columnBinary *= 2;
                        columnCounter++;
                    }

                    rowValues.Add(rowCounter, rowValue);

                    possibleColumns = possibleColumns.Where(c => Mirrors(columnValues, c))
                        .ToList();
                    possibleRows = possibleRows.Where(r => Mirrors(rowValues, r))
                        .ToList();

                    rowBinary *= 2;
                    rowCounter++;

                    inputString = await file.ReadLineAsync()
                        ?? throw new Exception("No string found");
                }
                var row = possibleRows.SingleOrDefault();
                var column = possibleColumns.SingleOrDefault();
                sum1 += row * 100 + column;

                var smudgeFound = false;
                for (var i = 1; i < rowValues.Count && !smudgeFound; i++)
                {
                    var differences = HammingDistances(rowValues, i);
                    if (differences.Count == 1)
                    {
                        var difference = differences.Single();
                        for (var j = 0; j < columnValues.Count; j++)
                        {
                            if (difference.Value == 1)
                            {
                                smudgeFound = true;
                                sum2 += i * 100;
                                break;
                            }
                        }
                    }
                }

                for (var i = 1; i < columnValues.Count && !smudgeFound; i++)
                {
                    var hammingDistances = HammingDistances(columnValues, i);
                    if (hammingDistances.Count == 1)
                    {
                        var hammingDistance = hammingDistances.Single();
                        for (var j = 0; j < rowValues.Count; j++)
                        {
                            if (hammingDistance.Value == 1)
                            {
                                smudgeFound = true;
                                sum2 += i;
                                break;
                            }
                        }
                    }
                }

                if (!smudgeFound)
                {
                    throw new Exception("No smudge found");
                }
            }
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    private static int CalculateHammingDistance(int val1, int val2)
    {
        string bin1 = Convert.ToString(val1, 2);
        string bin2 = Convert.ToString(val2, 2);

        var pad = Math.Max(bin1.Length, bin2.Length);
        bin1 = bin1.PadLeft(pad, '0');
        bin2 = bin2.PadLeft(pad, '0');

        var result = 0;
        for (int i = 0; i < pad; i++)
        {
            if (bin1[i] != bin2[i])
            {
                result++;
            }
        }
        return result;
    }

    private static Dictionary<int, int> HammingDistances(Dictionary<int, int> values, int index)
    {
        if (index < 1 && index >= values.Count)
        {
            throw new InvalidOperationException();
        }

        var result = new Dictionary<int, int>();

        for (int i = 0; index + i < values.Count && index - i - 1 >= 0; i++)
        {
            if (values[index + i] != values[index - i - 1])
            {
                result.Add(i, CalculateHammingDistance(values[index + i], values[index - i - 1]));
            }
        }

        return result;
    }

    private static bool Mirrors(Dictionary<int, int> values, int index)
    {
        for (int i = 0; index + i < values.Count && index - i - 1 >= 0; i++)
        {
            if (values[index + i] != values[index - i - 1])
            {
                return false;
            }
        }
        return index > 0 && index < values.Count;
    }

    #endregion Public Methods
}