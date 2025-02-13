using System.Diagnostics.Metrics;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.MovementStrategies
{
    public class PrayMovement : IMovement
    {
        public bool Move(Animal animal, IMap map)
        {
            var lions = map.Animals.Where(a => a is Lion && animal.DistanceTo(a.Position.X, a.Position.Y) <= animal.Vision);

            if (!lions.Any()) return false;

            var bestPosition = GetBestFreeSpace(animal, lions, map);

            animal.Position.X = bestPosition.X;
            animal.Position.Y = bestPosition.Y;

            return true;
        }

        private Position GetBestFreeSpace(Animal pray, IEnumerable<Animal> hunters, IMap map)
        {
            int x = pray.Position.X;
            int y = pray.Position.Y;

            Position BestPosition = new Position(x, y);
            double bestDistanceFromAllAnimal = double.MinValue;

            int startX = Math.Max(0, x - pray.Speed);
            int endX = Math.Min(map.Width - 1, x + pray.Speed);

            int startY = Math.Max(0, y - pray.Speed);
            int endY = Math.Min(map.Height - 1, y + pray.Speed);

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (map.IsPositionValid(i, j))
                    {
                        var distanceToAnimals = hunters.Sum(h => h.DistanceTo(i, j));

                        if (distanceToAnimals > bestDistanceFromAllAnimal)
                        {
                            BestPosition = new Position(i, j);
                            bestDistanceFromAllAnimal = distanceToAnimals;
                        }
                    }
                }
            }

            return BestPosition;
        }
    }
}
