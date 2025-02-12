using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IAnimalFactory
    {
        Animal CreateAnimal(Type animalType, int x, int y);
    }
}
