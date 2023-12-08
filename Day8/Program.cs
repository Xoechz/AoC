using AoC2023.Day8.Models;

namespace AoC2023.Day8;

public partial class Program
{
    #region Public Methods

    public static async Task Main(string[] args)
    {
        var steps1 = 0;
        long steps2 = 0;
        char[] instructions;
        List<Node> nodes = [];

        using (var file = File.OpenText("D:\\AoC2023\\InputFiles\\Day8.txt"))
        {
            var inputString = await file.ReadLineAsync()
                ?? throw new Exception("No string found");

            instructions = [.. inputString];

            while (!file.EndOfStream)
            {
                inputString = await file.ReadLineAsync()
                    ?? throw new Exception("No string found");
                if (!string.IsNullOrEmpty(inputString))
                {
                    nodes.Add(new Node(inputString));
                }
            }
        }
        var currentNode = nodes.Single(n => n.Id == "AAA");

        while (currentNode.Id != "ZZZ")
        {
            var instruction = instructions[steps1 % instructions.Length];

            if (instruction == 'R')
            {
                currentNode = GetRightNode(nodes, currentNode);
            }
            else if (instruction == 'L')
            {
                currentNode = GetLeftNode(nodes, currentNode);
            }
            else
            {
                throw new Exception("Invalid instructions");
            }

            steps1++;
        }
        Console.WriteLine("Task 1:");
        Console.WriteLine(steps1);

        List<long> loops = [];
        var currentNodes = nodes.Where(n => n.Id.EndsWith('A')).ToList();
        foreach (var node in currentNodes)
        {
            bool loopFound = false;
            currentNode = node;
            Dictionary<long, string> zNodes = [];
            int steps = 0;
            while (!loopFound)
            {
                var instruction = instructions[steps % instructions.Length];

                if (instruction == 'R')
                {
                    currentNode = GetRightNode(nodes, currentNode);
                }
                else if (instruction == 'L')
                {
                    currentNode = GetLeftNode(nodes, currentNode);
                }
                else
                {
                    throw new Exception("Invalid instructions");
                }
                steps++;

                if (currentNode.Id.EndsWith('Z'))
                {
                    zNodes.Add(steps, currentNode.Id);
                    var sameNodeId = zNodes.Where(z => z.Value == currentNode.Id)
                        .ToList();
                    if (sameNodeId.Count > 1)
                    {
                        var loopLenght = sameNodeId[1].Key - sameNodeId[0].Key;
                        if (loopLenght != sameNodeId[0].Key)
                        {
                            throw new Exception("This idea doesnt work");
                        }
                        loops.Add(loopLenght);
                        loopFound = true;
                    }
                }
            }
        }

        steps2 = loops.Aggregate(LCM);

        //Brute force:
        //while (currentNodes.Any(n => !n.Id.EndsWith('Z')))
        //{
        //    var instruction = instructions[steps2 % instructions.Length];
        //    if (instruction == 'R')
        //    {
        //        currentNodes = currentNodes.Select(n => GetRightNode(nodes, n)).ToList();
        //    }
        //    else if (instruction == 'L')
        //    {
        //        currentNodes = currentNodes.Select(n => GetLeftNode(nodes, n)).ToList();
        //    }
        //    else
        //    {
        //        throw new Exception("Invalid instructions");
        //    }
        //    steps2++;
        //}

        Console.WriteLine("Task 2:");
        Console.WriteLine(steps2);
    }

    private static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static Node GetLeftNode(List<Node> nodes, Node node)
    {
        node.LeftNode ??= nodes.Single(n => n.Id == node.Left);
        return node.LeftNode;
    }

    private static Node GetRightNode(List<Node> nodes, Node node)
    {
        node.RightNode ??= nodes.Single(n => n.Id == node.Right);
        return node.RightNode;
    }

    private static long LCM(long a, long b)
    {
        return (a / GCD(a, b)) * b;
    }

    #endregion Public Methods
}