using System.Text.RegularExpressions;

namespace AoC2023.Day12.Models;

public partial class Spring(string id, List<int> counts, long value = 1)
{
    #region Public Properties

    public List<int> Counts { get; set; } = [.. counts];
    public string Id { get; set; } = id;
    public long Value { get; set; } = value;

    #endregion Public Properties

    #region Public Methods

    public bool IsValid()
    {
        var countsCopy = Counts.ToList();
        var count = 0;
        foreach (var c in Id)
        {
            if (c == '?')
            {
                if (countsCopy.Count == 0)
                {
                    return true;
                }
                if (countsCopy[0] >= count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (c == '#')
            {
                count++;
                if (countsCopy.Count == 0)
                {
                    return false;
                }
            }
            else if (count > 0)
            {
                if (countsCopy[0] == count)
                {
                    countsCopy.RemoveAt(0);
                    count = 0;
                }
                else
                {
                    return false;
                }
            }
        }

        return countsCopy.Count == 0
            || (countsCopy.Count == 1
                && countsCopy[0] == count);
    }

    public void Shorten()
    {
        var count = 0;
        var index = 0;
        var removeTo = 0;
        foreach (var c in Id)
        {
            if (c == '?')
            {
                break;
            }
            else if (c == '#')
            {
                count++;
            }
            else if (count > 0)
            {
                if (Counts[0] == count)
                {
                    Counts.RemoveAt(0);
                    removeTo = index;
                    count = 0;
                }
                else
                {
                    break;
                }
            }
            index++;
        }
        var regex = MultiplePoints();
        Id = regex.Replace(Id[removeTo..], ".").Trim('.');
    }

    #endregion Public Methods

    #region Private Methods

    [GeneratedRegex("\\.+")]
    private static partial Regex MultiplePoints();

    #endregion Private Methods
}