using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.Animals
{
    public abstract class Animal(int id, int x, int y, string name, int speed, int vision, IMovement movement)
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public int Speed { get; } = speed;
        public int Vision { get; } = vision;
        public bool IsAlive { get; private set; } = true;
        public Position Position { get; } = new Position(x, y);
        private IMovement _movement { get; set; } = movement;

        public double DistanceTo(int x, int y)
        {
            return Math.Max(Math.Abs(Position.X - x), Math.Abs(Position.Y - y));
        }

        public void Death()
        {
            IsAlive = false;
        }

        public Position GetBestFreeSpace(Animal other, IMap map, Func<double, double, bool> comparator)
        {
            int x = Position.X;
            int y = Position.Y;

            Position BestPosition = new Position(x, y);
            double bestDistanceToAnimal = DistanceTo(other.Position.X, other.Position.Y);

            int startX = Math.Max(0, x - Speed);
            int endX = Math.Min(map.Width, x + Speed);

            int startY = Math.Max(0, y - Speed);
            int endY = Math.Min(map.Height, y + Speed);

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    if (map.IsPositionValid(i, j))
                    {
                        var distanceToAnimal = DistanceTo(i, j);

                        if (comparator(distanceToAnimal, bestDistanceToAnimal))
                        {
                            BestPosition = new Position(i, j);
                            bestDistanceToAnimal = distanceToAnimal;
                        }
                    }
                }
            }

            return BestPosition;
        }

        public void Move(IMap map)
        {
            var action = _movement.Move(this, map);

            if (!action)
            {
                var random = new RandomMovement();
                random.Move(this, map);
            }
        }
    }
}
