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
            var prays = map.Animals.Where(a => a is Pray && animal.DistanceTo(a.Position.X, a.Position.Y) <= animal.Features.Vision);

            if (!prays.Any()) return false;

            var prayToCatch = prays.OrderBy(a => animal.DistanceTo(a.Position.X, a.Position.Y)).First();

            if (animal.DistanceTo(prayToCatch.Position.X, prayToCatch.Position.Y) <= animal.Features.Speed)
            {
                int x = prayToCatch.Position.X;
                int y = prayToCatch.Position.Y;

                prayToCatch.Death();

                animal.IncreaseHealth(GameConstants.HealthIncreaseOnPrayEaten);

                animal.Position.X = x; animal.Position.Y = y;
            }
            else
            {
                var bestPosition = mapManager.GetBestFreeSpaceForHunter(animal, prayToCatch, map);

                animal.Position.X = bestPosition.X;
                animal.Position.Y = bestPosition.Y;
            }

            return true;
        }


    }
}
