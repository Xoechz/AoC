using System.Drawing;

namespace AoC2023.Day16.Models;

public class LightMap
{
    #region Public Properties

    public List<Point> HeatedPoints { get; set; } = [];
    public Dictionary<Point, char> Map { get; set; } = [];
    public int MaxColumn { get; set; }

    public int MaxRow { get; set; }

    public int CalculateHeatedPoints(Light initialLight)
    {
        HeatedPoints = [];
        Lights = [];

        var nextLights = ShineLight(initialLight);

        while (nextLights.Count > 0)
        {
            var currentLights = nextLights;
            nextLights = [];
            foreach (var light in currentLights)
            {
                nextLights.AddRange(ShineLight(light));
            }
        }

        HeatedPoints = HeatedPoints.Distinct()
                .ToList();

        return HeatedPoints.Count;
    }

    #endregion Public Properties

    #region Public Methods

    public List<Light> Lights { get; set; } = [];

    public Dictionary<int, char> GetColumn(int x) => Map.Where(m => m.Key.X == x).ToDictionary(m => m.Key.Y, m => m.Value);

    public Dictionary<int, char> GetRow(int y) => Map.Where(m => m.Key.Y == y).ToDictionary(m => m.Key.X, m => m.Value);

    public List<Light> ShineLight(Light light)
    {
        if (Lights.Contains(light))
        {
            return [];
        }
        Lights.Add(light);

        var cachedLight = CachedLights.FirstOrDefault(l => l.Equals(light));

        if (cachedLight != null)
        {
            HeatedPoints.AddRange(cachedLight.HeatedPoints);
            return cachedLight.NextLights;
        }

        Dictionary<int, char> line;
        int nextStep;
        int max;
        int start;
        int lineNumber;
        if (light.Direction == Direction.North)
        {
            lineNumber = light.X;
            line = GetColumn(light.X);
            nextStep = -1;
            max = MaxColumn;
            start = light.Y - 1;
        }
        else if (light.Direction == Direction.East)
        {
            lineNumber = light.Y;
            line = GetRow(light.Y);
            nextStep = 1;
            max = MaxRow;
            start = light.X + 1;
        }
        else if (light.Direction == Direction.South)
        {
            lineNumber = light.X;
            line = GetColumn(light.X);
            nextStep = 1;
            max = MaxColumn;
            start = light.Y + 1;
        }
        else
        {
            lineNumber = light.Y;
            line = GetRow(light.Y);
            nextStep = -1;
            max = MaxRow;
            start = light.X - 1;
        }

        var mirrorHit = false;
        for (int i = start; i >= 0 && i <= max && !mirrorHit; i += nextStep)
        {
            if (line.TryGetValue(i, out char value))
            {
                if (value == '/')
                {
                    if (light.Direction == Direction.North)
                    {
                        Console.WriteLine("/ " + lineNumber + " " + i);
                        light.NextLights.Add(new Light(lineNumber, i, Direction.East));
                        mirrorHit = true;
                    }
                    else if (light.Direction == Direction.East)
                    {
                        Console.WriteLine("/ " + i + " " + lineNumber);
                        light.NextLights.Add(new Light(i, lineNumber, Direction.North));
                        mirrorHit = true;
                    }
                    else if (light.Direction == Direction.South)
                    {
                        Console.WriteLine("/ " + lineNumber + " " + i);
                        light.NextLights.Add(new Light(lineNumber, i, Direction.West));
                        mirrorHit = true;
                    }
                    else
                    {
                        Console.WriteLine("/ " + i + " " + lineNumber);
                        light.NextLights.Add(new Light(i, lineNumber, Direction.South));
                        mirrorHit = true;
                    }
                }
                else if (value == '\\')
                {
                    if (light.Direction == Direction.North)
                    {
                        Console.WriteLine("\\ " + lineNumber + " " + i);
                        light.NextLights.Add(new Light(lineNumber, i, Direction.West));
                        mirrorHit = true;
                    }
                    else if (light.Direction == Direction.East)
                    {
                        Console.WriteLine("\\ " + i + " " + lineNumber);
                        light.NextLights.Add(new Light(i, lineNumber, Direction.South));
                        mirrorHit = true;
                    }
                    else if (light.Direction == Direction.South)
                    {
                        Console.WriteLine("\\ " + lineNumber + " " + i);
                        light.NextLights.Add(new Light(lineNumber, i, Direction.East));
                        mirrorHit = true;
                    }
                    else
                    {
                        Console.WriteLine("\\ " + i + " " + lineNumber);
                        light.NextLights.Add(new Light(i, lineNumber, Direction.North));
                        mirrorHit = true;
                    }
                }
                else if (value == '-')
                {
                    if (light.Direction == Direction.North || light.Direction == Direction.South)
                    {
                        Console.WriteLine("- " + lineNumber + " " + i);
                        light.NextLights.Add(new Light(lineNumber, i, Direction.East));
                        light.NextLights.Add(new Light(lineNumber, i, Direction.West));
                        mirrorHit = true;
                    }
                }
                else if (value == '|')
                {
                    if (light.Direction == Direction.East || light.Direction == Direction.West)
                    {
                        Console.WriteLine("| " + i + " " + lineNumber);
                        light.NextLights.Add(new Light(i, lineNumber, Direction.North));
                        light.NextLights.Add(new Light(i, lineNumber, Direction.South));
                        mirrorHit = true;
                    }
                }
            }

            if (light.Direction == Direction.North || light.Direction == Direction.South)
            {
                light.HeatedPoints.Add(new Point(lineNumber, i));
            }
            else if (light.Direction == Direction.East || light.Direction == Direction.West)
            {
                light.HeatedPoints.Add(new Point(i, lineNumber));
            }
        }

        HeatedPoints = HeatedPoints.Union(light.HeatedPoints)
            .Distinct()
            .ToList();

        CachedLights.Add(light);

        return light.NextLights;
    }

    #endregion Public Methods

    #region Private Properties

    private List<Light> CachedLights { get; set; } = [];

    #endregion Private Properties
}