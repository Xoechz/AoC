namespace AoC2023.Day5.Models
{
    public class Map(string[] mapStrings)
    {
        #region Public Properties

        public long Destination { get; set; } = long.Parse(mapStrings[0]);
        public long Range { get; set; } = long.Parse(mapStrings[2]);
        public long Source { get; set; } = long.Parse(mapStrings[1]);

        #endregion Public Properties
    }
}