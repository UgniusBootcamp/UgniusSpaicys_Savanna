using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Helpers.MovementStrategies
{
    public class HunterMovement(IMapManager mapManager) : IMovement
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

                animal.IncreaseHealth(GameConstants.HealthIncreaseOnAntelopeEaten);

                animal.Position.X = x; animal.Position.Y = y;
            }
            else
            {
                var bestPosition = mapManager.GetBestFreeSpaceForHunter(animal, antelopeToCatch, map);

                animal.Position.X = bestPosition.X;
                animal.Position.Y = bestPosition.Y;
            }

            return true;
        }


    }
}
