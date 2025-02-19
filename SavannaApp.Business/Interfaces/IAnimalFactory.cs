using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalFactory
    {
        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Created animal</returns>
        Animal CreateAnimal(Type animalType, int x, int y);
    }
}
