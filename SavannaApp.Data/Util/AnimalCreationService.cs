using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces.Map;

namespace SavannaApp.Data.Util
{
    public class AnimalCreationService(IAnimalFactory animalFactory, IMapManager mapManager) : IAnimalCreationService
    {

        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        /// <param name="map">Map</param>
        /// <returns>Created animal or null if there is not free space</returns>
        public Animal? CreateAnimal(Type animalType, IMap map)
        {
            var newPosition = mapManager.GetRandomFreePlaceOnMap(map);

            if (newPosition == null) return null;

            var newAnimal = animalFactory.CreateAnimal(animalType, newPosition.X, newPosition.Y);

            return newAnimal;
        }

        public Animal? RebirthAnimal(List<Animal> Animals, IMap map)
        {
            var position = mapManager.GetClosestFreePositionToAnimals(Animals, map);

            if (position == null) return null;

            var newBorn = animalFactory.CreateAnimal(Animals.First().GetType(), position.X, position.Y);

            return newBorn;
        }



    }
}
