using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.Configuration;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Tiger(int id, int x, int y, IMovement movement, AnimalConfig config) : Hunter(id, x, y, movement)
    {
        private AnimalFeatures _animalFeatures = new AnimalFeatures(config.Speed, config.Vision, config.Health);
        public override ConsoleKey CreationKey => (ConsoleKey)config.Key;
        public override string Name => config.Name;
        public override AnimalFeatures Features => _animalFeatures;
    }
}
