using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Factory
{
    public class AntelopeFactory : AnimalFactory
    {
        private readonly string _antelopeName = "A";
        private readonly int _antelopeSpeed = 5;
        private readonly int _antelopeVision = 2;
        public override Animal CreateAnimal(int id, int x, int y)
        {
            return new Antelope(id, x, y, _antelopeName, _antelopeSpeed, _antelopeVision);
        }
    }
}
