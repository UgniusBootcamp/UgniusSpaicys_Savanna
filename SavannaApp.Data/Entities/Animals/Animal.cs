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

        public abstract void NextPosition(IMap map);

        protected double DistanceTo(Animal animal)
        {
            return Math.Sqrt(Math.Pow(animal.Position.X - Position.X, 2) + Math.Pow(animal.Position.Y - Position.Y, 2));
        }

        protected Animal? FindClosestAnimal(Type animalType, IMap map)
        {
            Animal? closestAnimal = null;
            double minDistance = double.MaxValue;

            int startX = Math.Max(0, Position.X - Vision);
            int endX = Math.Min(map.Width - 1, Position.X + Vision);

            int startY = Math.Max(0, Position.Y - Vision);
            int endY = Math.Min(map.Height - 1, Position.Y + Vision);

            Parallel.For(startX, endX, i =>
            {
                for (int j = startY; j < endY; j++)
                {
                    var animal = map.GetAnimal(i, j);

                    if (animal == null) continue;

                    if (animal.GetType() == animalType)
                    {
                        double distance = DistanceTo(animal);

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestAnimal = animal;
                        }
                    }
                }
            });

            return closestAnimal;
        }

        public void SetPosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
    }
}
