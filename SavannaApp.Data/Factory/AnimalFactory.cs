using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Factory
{
    public class AnimalFactory : IAnimalFactory
    {
        private int _id = 1;
        public AnimalFactory() { }
        public Animal CreateAnimal(Type animalType, int x, int y) 
        {
            switch (animalType)
            {
                case Type t when t == typeof(Lion):
                    return new Lion(_id++, x, y, "L", 3, 5);
                case Type t when t == typeof(Antelope):
                    return new Antelope(_id++, x, y, "A", 5, 2);
                default:
                    throw new ArgumentException("Unknown animal type to create", nameof(animalType));
            }
        }
    }
}
