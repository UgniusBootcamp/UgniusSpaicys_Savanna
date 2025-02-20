using System;

namespace SavannaApp.Data.Entities.Animals
{
    public class AnimalFeatures(int speed, int vision, double health)
    {
        public int Speed { get; set; } = speed;
        public int Vision { get; set;  } = vision;
        public double Health { get; set; } = health;
    }
}
