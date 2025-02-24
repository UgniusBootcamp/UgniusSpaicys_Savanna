using SavannaApp.Animals.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Tiger(int id, int x, int y, IMovement movement)
        : Hunter(id, x, y, AnimalConstants.Tiger, new AnimalFeatures(AnimalConstants.TigerSpeed, AnimalConstants.TigerVision, AnimalConstants.TigerHealth), movement)
    {
        public override ConsoleKey CreationKey => ConsoleKey.T;
    }
}
