using SavannaApp.Animals.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Lion(int id, int x, int y, IMovement movement) : Hunter(id, x, y, movement)
    {
        private AnimalFeatures _animalFeatures = new AnimalFeatures(AnimalConstants.LionSpeed, AnimalConstants.LionVision, AnimalConstants.LionHealth);
        public override ConsoleKey CreationKey => ConsoleKey.L;
        public override string Name => AnimalConstants.Lion;
        public override AnimalFeatures Features => _animalFeatures;
    }
}
