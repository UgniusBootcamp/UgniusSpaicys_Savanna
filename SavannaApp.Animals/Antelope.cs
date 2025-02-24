using SavannaApp.Animals.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Antelope(int id, int x, int y, IMovement movement)
        : Pray(id, x, y, AnimalConstants.Antelope, new AnimalFeatures(AnimalConstants.AntelopeSpeed, AnimalConstants.AntelopeVision, AnimalConstants.AntelopeHealth), movement)
    {
        public override ConsoleKey CreationKey => ConsoleKey.A;
    }
}
