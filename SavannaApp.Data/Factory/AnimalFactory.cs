﻿using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Factory
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
            switch (animalType)
            {
                case Type t when t == typeof(Lion):
                    return new Lion(_id++, x, y, GameConstants.Lion, GameConstants.LionSpeed, GameConstants.LionVision, GameConstants.LionHealth, hunter);
                case Type t when t == typeof(Antelope):
                    return new Antelope(_id++, x, y, GameConstants.Antelope, GameConstants.AntelopeSpeed, GameConstants.AntelopeVision, GameConstants.AntelopeHealth, pray);
                default:
                    throw new ArgumentException(GameConstants.UnknownAnimalType, nameof(animalType));
            }
        }
    }
}
