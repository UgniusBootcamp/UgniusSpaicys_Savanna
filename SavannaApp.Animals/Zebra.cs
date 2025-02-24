using SavannaApp.Animals.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Zebra(int id, int x, int y, IMovement movement)
        : Pray(id, x, y, AnimalConstants.Zebra, new AnimalFeatures(AnimalConstants.ZebraSpeed, AnimalConstants.ZebraVision, AnimalConstants.ZebraHealth), movement)
    {
        public override ConsoleKey CreationKey => ConsoleKey.Z;
    }
}
