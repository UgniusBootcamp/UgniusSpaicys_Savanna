﻿using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Helpers.MovementStrategies
{
    public class PrayMovement(IMapManager mapManager) : IMovement
    {
        /// <summary>
        /// Method for pray to escape from hunters
        /// </summary>
        /// <param name="animal">pray</param>
        /// <param name="map">Map</param>
        /// <returns>true if animal has moved, false if not</returns>
        public bool Move(Animal animal, IMap map)
        {
            var hunters = map.Animals.Where(a => a is Hunter && animal.DistanceTo(a.Position.X, a.Position.Y) <= animal.Features.Vision);

            if (!hunters.Any()) return false;

            var bestPosition = mapManager.GetBestFreeSpaceForPray(animal, hunters, map);

            animal.Position.X = bestPosition.X;
            animal.Position.Y = bestPosition.Y;

            return true;
        }


    }
}
