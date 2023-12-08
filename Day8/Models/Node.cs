namespace AoC2023.Day8.Models;

public class Node
{
    #region Public Constructors

    public Node(string nodeString)
    {
        if (nodeString.Length != 16)
        {
            throw new Exception("Invalid input string");
        }
        Id = nodeString[..3];
        Left = nodeString[7..10];
        Right = nodeString[12..15];
    }

    #endregion Public Constructors

    #region Public Properties

    public string Id { get; set; }
    public string Left { get; set; }
    public Node? LeftNode { get; set; }
    public string Right { get; set; }
    public Node? RightNode { get; set; }

    #endregion Public Properties
}