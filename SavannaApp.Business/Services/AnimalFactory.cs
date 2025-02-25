using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class AnimalFactory(HunterMovement hunter, PrayMovement pray, IAnimalConfigurationService configurationService) : IAnimalFactory
    {
        private int _id = 1;

        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="animalType">type of animal</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Created animal</returns>
        /// <exception cref="ArgumentException">if animal type is not found</exception>
        public Animal CreateAnimal(Type animalType, int x, int y) 
        {
            IMovement movement = animalType.BaseType == typeof(Hunter) ? hunter : pray;
            var config = configurationService.GetConfig(animalType.Name);

            var animal = Activator.CreateInstance(animalType, _id++, x, y, movement, config) as Animal;

            if (animal == null)
                throw new Exception(String.Format("{0} {1}", GameConstants.NoConstructor, animalType.ToString()));

            return animal;
        }
    }
}
