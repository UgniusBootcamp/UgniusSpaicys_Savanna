using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Helpers.Map
{
    public class MapManager : IMapManager
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Method to get random free place on map
        /// </summary>
        /// <param name="map">random free place on map</param>
        /// <returns>null if map is full, free place otherwise</returns>
        public Position? GetRandomFreePlaceOnMap(IMap map)
        {
            if (map.Animals.Count() == map.Height * map.Width) return null;

            int x;
            int y;

            do
            {
                x = _random.Next(0, map.Width);
                y = _random.Next(0, map.Height);

            } while (!map.IsPositionValid(x, y));

            return new Position(x, y);
        }

        /// <summary>
        /// Helper method to get best free position which is closest to pray
        /// </summary>
        /// <param name="hunter">hunter</param>
        /// <param name="pray">pray</param>
        /// <param name="map">Map</param>
        /// <returns>Free closest to pray position</returns>
        public Position GetBestFreeSpaceForHunter(Animal hunter, Animal pray, IMap map)
        {
            int x = hunter.Position.X;
            int y = hunter.Position.Y;

            Position BestPosition = new Position(x, y);
            double bestDistanceToAnimal = double.MaxValue;

            int startX = Math.Max(0, x - hunter.Features.Speed);
            int endX = Math.Min(map.Width - 1, x + hunter.Features.Speed);

            int startY = Math.Max(0, y - hunter.Features.Speed);
            int endY = Math.Min(map.Height - 1, y + hunter.Features.Speed);

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (map.IsPositionValid(i, j))
                    {
                        var distanceToAnimal = pray.DistanceTo(i, j);

                        if (distanceToAnimal < bestDistanceToAnimal)
                        {
                            BestPosition = new Position(i, j);
                            bestDistanceToAnimal = distanceToAnimal;
                        }
                    }
                }
            }

            return BestPosition;
        }

        /// <summary>
        /// Helper method to calculate best free position which is furthest from all nearby hunters
        /// </summary>
        /// <param name="pray">pray</param>
        /// <param name="hunters">hunters</param>
        /// <param name="map">Map</param>
        /// <returns>furthest position from all hunters</returns>
        public Position GetBestFreeSpaceForPray(Animal pray, IEnumerable<Animal> hunters, IMap map)
        {
            int x = pray.Position.X;
            int y = pray.Position.Y;

            Position BestPosition = new Position(x, y);
            double bestDistanceFromAllAnimal = double.MinValue;

            int startX = Math.Max(0, x - pray.Features.Speed);
            int endX = Math.Min(map.Width - 1, x + pray.Features.Speed);

            int startY = Math.Max(0, y - pray.Features.Speed);
            int endY = Math.Min(map.Height - 1, y + pray.Features.Speed);

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

        /// <summary>
        /// Method to get closest position to animals
        /// </summary>
        /// <param name="animals">animals</param>
        /// <param name="map">map</param>
        /// <returns>closest position to animals</returns>
        public Position? GetClosestFreePositionToAnimals(List<Animal> animals, IMap map)
        {
            int x = (int)animals.Average(a => a.Position.X);
            int y = (int)animals.Average(a => a.Position.Y);

            if (map.IsPositionValid(x, y))
            {
                return new Position(x, y);
            }

            for (int radius = 1; radius < Math.Max(map.Width, map.Height); radius++)
            {
                for (int dx = -radius; dx <= radius; dx++)
                {
                    for (int dy = -radius; dy <= radius; dy++)
                    {
                        int newX = x + dx;
                        int newY = y + dy;

                        if (newX >= 0 && newX < map.Width && newY >= 0 && newY < map.Height)
                        {
                            if (map.IsPositionValid(newX, newY))
                            {
                                return new Position(newX, newY);
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
