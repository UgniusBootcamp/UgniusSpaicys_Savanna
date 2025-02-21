using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class AnimalFactory(HunterMovement hunter, PrayMovement pray) : IAnimalFactory
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

            switch (animalType)
            {
                case Type t when t == typeof(Lion):
                    return new Lion(_id++, x, y, GameConstants.Lion, new AnimalFeatures(GameConstants.LionSpeed, GameConstants.LionVision, GameConstants.LionHealth), movement);
                case Type t when t == typeof(Antelope):
                    return new Antelope(_id++, x, y, GameConstants.Antelope, new AnimalFeatures(GameConstants.AntelopeSpeed, GameConstants.AntelopeVision, GameConstants.AntelopeHealth), movement);
                default:
                    throw new ArgumentException(GameConstants.UnknownAnimalType, nameof(animalType));
            }
        }
    }
}
