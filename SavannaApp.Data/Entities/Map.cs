using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities
{
    public class Map : IMap
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Animal[,] _map { get; private set; }

        public Map(int height, int width)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("Invalid");


            Height = height;
            Width = width;
            _map = new Animal[height, width];
        }

        public void SetAnimal(Animal animal)
        {
            int x = animal.Position.X;
            int y = animal.Position.Y;

            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new ArgumentException("Invalid Position");

            _map[y, x] = animal;
        }

        public Animal GetAnimal(int x, int y) 
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new ArgumentException("Invalid Position");

            return _map[y, x];
        }

        public List<Animal> Animals
        {
            get
            {
                List<Animal> animals = new List<Animal>();
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var animal = _map[y, x];

                        if (animal != null) animals.Add(animal);
                    }
                }
                return animals;
            }
        }
    }
}
