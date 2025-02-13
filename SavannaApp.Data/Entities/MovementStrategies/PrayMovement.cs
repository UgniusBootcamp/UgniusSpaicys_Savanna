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

            IDictionary<Position,int> bestPositionCounter = new Dictionary<Position,int>();

            foreach (var lion in lions) 
            {
                var position = animal.GetBestFreeSpace(lion, map, (close, far) => close > far);

                if (!bestPositionCounter.ContainsKey(position))
                {
                    bestPositionCounter.Add(position, 1);
                }
                else
                {
                    bestPositionCounter[position]++;
                }
            }

            var bestPosition = bestPositionCounter.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            animal.Position.X = bestPosition.X;
            animal.Position.Y = bestPosition.Y;

            return true;
        }
    }
}
