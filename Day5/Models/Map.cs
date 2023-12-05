namespace AoC2023.Day5.Models
{
    public class Map(string[] mapStrings)
    {
        public long Source { get; set; } = long.Parse(mapStrings[1]);
        public long Destination { get; set; } = long.Parse(mapStrings[0]);
        public long Range { get; set; } = long.Parse(mapStrings[2]);
    }
}