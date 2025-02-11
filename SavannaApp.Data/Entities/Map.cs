using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities
{
    public class Map : IMap
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Animal[,] _map { get; private set; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            _map = new Animal[Height, Width];
        }

        public void SetAnimal(Animal animal)
        {
            int x = animal.X;
            int y = animal.Y;

            if (x < 0 || y < 0 || x >= Height || y >= Width)
                throw new ArgumentException("Invalid Position");

            _map[animal.X, animal.Y] = animal;
        }

        public Animal GetAnimal(int x, int y) 
        {
            if (x < 0 || y < 0 || x >= Height || y >= Width)
                throw new ArgumentException("Invalid Position");

            return _map[x, y];
        }
    }
}
