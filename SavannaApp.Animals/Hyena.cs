using SavannaApp.Animals.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Hyena(int id, int x, int y, IMovement movement) : Hunter(id, x, y, movement)
    {
        private AnimalFeatures _animalFeatures = new AnimalFeatures(AnimalConstants.HyenaSpeed, AnimalConstants.HyenaVision, AnimalConstants.HyenaHealth);
        public override ConsoleKey CreationKey => ConsoleKey.H;
        public override string Name => AnimalConstants.Hyena;
        public override AnimalFeatures Features => _animalFeatures;

    }
}
