using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.Configuration;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Animals
{
    public class Antelope(int id, int x, int y, IMovement movement, AnimalConfig config) : Pray(id, x, y, movement)
    {
        private AnimalFeatures _animalFeatures = new AnimalFeatures(config.Speed, config.Vision, config.Health);
        public override ConsoleKey CreationKey => (ConsoleKey)config.Key;

        public override string Name => config.Name;

        public override AnimalFeatures Features => _animalFeatures;
        public override string? Icon => config.Icon;
    }
}
