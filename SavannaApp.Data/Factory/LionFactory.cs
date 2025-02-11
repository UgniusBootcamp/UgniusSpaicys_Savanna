using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Factory
{
    public class LionFactory : AnimalFactory
    {
        private readonly string _lionName = "L";
        private readonly int _lionSpeed = 2;
        private readonly int _lionVision = 5;
        public override Animal CreateAnimal(int id, int x, int y)
        {
            return new Lion(id, x, y, _lionName, _lionSpeed, _lionVision);
        }
    }
}
