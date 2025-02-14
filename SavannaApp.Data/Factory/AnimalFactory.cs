using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Factory
{
    public class AnimalFactory : IAnimalFactory
    {
        private int _id = 1;

        /// <summary>
        /// Animal factory constructor
        /// </summary>
        public AnimalFactory() { }

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
            switch (animalType)
            {
                case Type t when t == typeof(Lion):
                    return new Lion(_id++, x, y, GameConstants.Lion, GameConstants.LionSpeed, GameConstants.LionVision, GameConstants.LionHealth, new HunterMovement());
                case Type t when t == typeof(Antelope):
                    return new Antelope(_id++, x, y, GameConstants.Antelope, GameConstants.AntelopeSpeed, GameConstants.AntelopeVision, GameConstants.AntelopeHealth, new PrayMovement());
                default:
                    throw new ArgumentException(GameConstants.UnknownAnimalType, nameof(animalType));
            }
        }
    }
}
