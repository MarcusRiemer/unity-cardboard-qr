namespace Assets.Scripts
{
    /// <summary>
    /// Class representing a position.
    /// </summary>
    public class Position
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string[] Arrows { get; set; }

        public Position(int id, string text, string[] arrows)
        {
            Id = id;
            Text = text;
            Arrows = arrows;
        }
    }
}