namespace SavannaApp.Data.Entities.Animals
{
    public class AnimalFeatures(int speed, int vision, double health)
    {
        public int Speed { get; set; } = speed;
        public int Vision { get; set;  } = vision;
        public double Health { get; set; } = health;
        public int Age { get; set; } = 0;
        public int Offsprings { get; set; } = 0;
    }
}
