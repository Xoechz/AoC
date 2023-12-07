using System.Collections.ObjectModel;

namespace AoC2023.Day7;

public partial class Program
{
    private static readonly ReadOnlyDictionary<char, int?> CardValues = new(new Dictionary<char, int?>
    {
        { '2', 0 },
        { '3', 1 },
        { '4', 2 },
        { '5', 3 },
        { '6', 4 },
        { '7', 5 },
        { '8', 6 },
        { '9', 7 },
        { 'T', 8 },
        { 'J', 9 },
        { 'Q', 10 },
        { 'K', 11 },
        { 'A', 12 },
    });

    private static readonly ReadOnlyDictionary<char, int?> CardValuesWithJokers = new(new Dictionary<char, int?>
    {
        { 'J', 0 },
        { '2', 1 },
        { '3', 2 },
        { '4', 3 },
        { '5', 4 },
        { '6', 5 },
        { '7', 6 },
        { '8', 7 },
        { '9', 8 },
        { 'T', 9 },
        { 'Q', 10 },
        { 'K', 11 },
        { 'A', 12 },
    });

    public static async Task Main(string[] args)
    {
        Dictionary<int, int> hands1 = [];
        Dictionary<int, int> hands2 = [];
        var sum1 = 0;
        var sum2 = 0;
        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day7.txt"))
        {
            while (!file.EndOfStream)
            {
                var inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                var parts = inputString.Split(' ');
                if (parts.Length != 2)
                {
                    throw new Exception("Invalid input string format");
                }
                var bid = int.Parse(parts[1]);

                //i basiclly created a base 13 counting system for card values
                var value1 = GetHandValue(parts[0]);
                var value2 = GetHandValueWithJokers(parts[0]);
                hands1.Add(value1, bid);
                hands2.Add(value2, bid);
            }
        }

        hands1 = hands1.OrderBy(h => h.Key).ToDictionary();
        hands2 = hands2.OrderBy(h => h.Key).ToDictionary();

        var rank = 1;
        foreach (var h in hands1)
        {
            sum1 += h.Value * rank;
            rank++;
        }

        rank = 1;
        foreach (var h in hands2)
        {
            sum2 += h.Value * rank;
            rank++;
        }

        Console.WriteLine("Task 1:");
        Console.WriteLine(sum1);
        Console.WriteLine("Task 2:");
        Console.WriteLine(sum2);
    }

    private static int GetHandValue(string cards)
    {
        var value = 0;

        if (cards.Length != 5)
        {
            throw new Exception("Invalid hand format");
        }

        for (int i = 0; i < 5; i++)
        {
            value += GetCardValue(cards[4 - i]) * (int)Math.Pow(13, i);
        }

        var multiplier = (int)Math.Pow(13, 5);
        var cardCounts = GetCardCounts(cards);

        if (cardCounts.Any(c => c.Value == 5))
        {
            value += 6 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value == 4))
        {
            value += 5 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value == 3) && cardCounts.Any(c => c.Value == 2))
        {
            value += 4 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value == 3))
        {
            value += 3 * multiplier;
        }
        else if (cardCounts.Count(c => c.Value == 2) == 2)
        {
            value += 2 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value == 2))
        {
            value += multiplier;
        }
        else
        {
            //Value does not change
        }
        return value;
    }

    private static int GetHandValueWithJokers(string cards)
    {
        var value = 0;

        if (cards.Length != 5)
        {
            throw new Exception("Invalid hand format");
        }

        for (int i = 0; i < 5; i++)
        {
            value += GetCardValueWithJokers(cards[4 - i]) * (int)Math.Pow(13, i);
        }

        var multiplier = (int)Math.Pow(13, 5);
        var cardCounts = GetCardCounts(cards);

        var jokers = cardCounts['J'];
        cardCounts.Remove('J');

        if (cardCounts.Any(c => c.Value >= 5 - jokers))
        {
            value += 6 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value >= 4 - jokers))
        {
            value += 5 * multiplier;
        }
        else if (CheckFullHouseWithJokers(cardCounts, jokers))
        {
            value += 4 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value >= 3 - jokers))
        {
            value += 3 * multiplier;
        }
        else if (cardCounts.Count(c => c.Value >= 2 - jokers) == 2)
        {
            value += 2 * multiplier;
        }
        else if (cardCounts.Any(c => c.Value >= 2 - jokers))
        {
            value += multiplier;
        }
        else
        {
            //Value does not change
        }
        return value;
    }

    private static bool CheckFullHouseWithJokers(Dictionary<char, int> cardCounts, int jokers)
    {
        //sort out higher scoring types
        if (cardCounts.Any(c => c.Value >= 4 - jokers))
        {
            return false;
        }

        var tripletsWithJoker = cardCounts.Where(c => c.Value >= 3 - jokers);
        if (!tripletsWithJoker.Any())
        {
            return false;
        }
        var triplet = tripletsWithJoker.First();

        return cardCounts.Any(c => c.Value == 2 && c.Key != triplet.Key);
    }

    private static int GetCardValue(char card) =>
        CardValues.GetValueOrDefault(card)
            ?? throw new Exception("Invalid card");

    private static int GetCardValueWithJokers(char card) =>
        CardValuesWithJokers.GetValueOrDefault(card)
            ?? throw new Exception("Invalid card");

    private static Dictionary<char, int> GetCardCounts(string cards)
    {
        Dictionary<char, int> result = [];

        foreach (var cardChar in CardValues.Keys)
        {
            result.Add(cardChar, cards.Count(c => c == cardChar));
        }

        return result;
    }
}