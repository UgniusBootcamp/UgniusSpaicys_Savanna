using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Entities.MovementStrategies
{
    public class HunterMovement : IMovement
    {
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
                var bestPosition = animal.GetBestFreeSpace(antelopeToCatch, map, (close, far) => close < far);


                animal.Position.X = bestPosition.X;
                animal.Position.Y = bestPosition.Y;
            }

            return true;
        }
    }
}
