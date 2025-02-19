using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalCreationService
    {
        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        /// <param name="map">Map</param>
        /// <returns>Created animal or null if there is no free space in map</returns>
        public Animal? CreateAnimal(Type animalType, IMap map);

        public Animal? RebirthAnimal(List<Animal> Animals, IMap map);
    }
}
