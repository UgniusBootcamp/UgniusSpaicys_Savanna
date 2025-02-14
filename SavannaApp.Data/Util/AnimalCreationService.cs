using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Util
{
    public class AnimalCreationService(IAnimalFactory animalFactory) : IAnimalCreationService
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        /// <param name="map">Map</param>
        /// <returns>Created animal or null if there is not free space</returns>
        public Animal? CreateAnimal(Type animalType, IMap map)
        {
            var newPosition = GetRandomFreePlaceOnMap(map);

            if (newPosition == null) return null;

            var newAnimal = animalFactory.CreateAnimal(animalType, newPosition.X, newPosition.Y);

            return newAnimal;
        }

        private Position? GetRandomFreePlaceOnMap(IMap map)
        {
            if (map.Animals.Count() == map.Height * map.Width) return null;

            int x;
            int y;

            do
            {
                x = _random.Next(0, map.Width);
                y = _random.Next(0, map.Height);

            } while (!map.IsPositionValid(x,y));

            return new Position(x, y);
        }

    }
}
