using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal
    {
        public int Id { get; }
        public string Name { get; }
        public int Speed { get; }
        public int Vision { get; }
        public Position Position { get; }

        public Animal(int id, int x, int y, string name, int speed, int vision)
        {
            Id = id;
            Speed = speed;
            Vision = vision;
            Name = name;
            Position = new Position(x, y);
        }
    }
}
