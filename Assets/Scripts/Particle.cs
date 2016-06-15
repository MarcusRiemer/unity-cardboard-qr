namespace Assets.Scripts
{
    /// <summary>
    /// Class representing a particlesystem.
    /// </summary>
    public class Particle
    {
        public int Id { get; set; }
        public string StartColor { get; set; }
        public string EndColor { get; set; }

        public Particle(int id, string startColor, string endColor)
        {
            Id = id;
            StartColor = startColor;
            EndColor = endColor;
        }
    }
}