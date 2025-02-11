using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Factory
{
    public abstract class AnimalFactory
    {
        public AnimalFactory() { }
        public abstract Animal CreateAnimal(int id, int x, int y);
    }
}
