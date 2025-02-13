using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities
{
    public class Map : IMap
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public List<Animal> Animals { get; private set; } = new List<Animal>();

        public Map(int height, int width)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Invalid");


            Height = height;
            Width = width;
        }

        public void SetAnimal(Animal animal)
        {
            int x = animal.Position.X;
            int y = animal.Position.Y;

            if (x < 0 || y < 0 || x >= Width || y >= Height || Animals.Any(a => a.Position.X == x && a.Position.Y == y))
                throw new ArgumentException("Invalid Position");

            Animals.Add(animal);
        }

        public Animal? GetAnimal(int x, int y) 
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new ArgumentException("Invalid Position");

            return Animals.FirstOrDefault(a => a.Position.X == x && a.Position.Y == y);
        }

        public bool IsPositionValid(int x, int y)
        {
            return !(x < 0 || y < 0 || x >= Width || y >= Height || Animals.Any(a => a.Position.X == x && a.Position.Y == y));
        }

        public void RemoveDeadAnimals()
        {
            Animals.RemoveAll(a => !a.IsAlive);
        }
    }
}
