using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.MovementStrategies
{
    public class HunterMovement : IMovement
    {
        /// <summary>
        /// Method to catch pray or move towards best possible position towards pray if position is not reachable in one go
        /// </summary>
        /// <param name="animal">animal</param>
        /// <param name="map">map</param>
        /// <returns>true if movement was performed, false if not</returns>
        public bool Move(Animal animal, IMap map)
        {
            var antelopes = map.Animals.Where(a => a is Antelope && animal.DistanceTo(a.Position.X, a.Position.Y) <= animal.Vision);

            if (!antelopes.Any()) return false;

            var antelopeToCatch = antelopes.OrderBy(a => animal.DistanceTo(a.Position.X, a.Position.Y)).First();

            if (animal.DistanceTo(antelopeToCatch.Position.X, antelopeToCatch.Position.Y) <= animal.Speed)
            {
                int x = antelopeToCatch.Position.X;
                int y = antelopeToCatch.Position.Y;
                antelopeToCatch.Death();

                animal.Position.X = x; animal.Position.Y = y;
            }
            else
            {
                var bestPosition = GetBestFreeSpace(animal, antelopeToCatch, map);

                animal.Position.X = bestPosition.X;
                animal.Position.Y = bestPosition.Y;
            }

            return true;
        }

        /// <summary>
        /// Helper method to get best free position which is closest to pray
        /// </summary>
        /// <param name="hunter">hunter</param>
        /// <param name="pray">pray</param>
        /// <param name="map">Map</param>
        /// <returns>Free closest to pray position</returns>
        private Position GetBestFreeSpace(Animal hunter, Animal pray, IMap map)
        {
            int x = hunter.Position.X;
            int y = hunter.Position.Y;

            Position BestPosition = new Position(x, y);
            double bestDistanceToAnimal = hunter.DistanceTo(pray.Position.X, pray.Position.Y);

            int startX = Math.Max(0, x - hunter.Speed);
            int endX = Math.Min(map.Width - 1, x + hunter.Speed);

            int startY = Math.Max(0, y - hunter.Speed);
            int endY = Math.Min(map.Height - 1, y + hunter.Speed);

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (map.IsPositionValid(i, j))
                    {
                        var distanceToAnimal = hunter.DistanceTo(i, j);

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
    }
}
