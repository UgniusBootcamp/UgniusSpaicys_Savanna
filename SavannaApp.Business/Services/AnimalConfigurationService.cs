using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Services
{
    public class AnimalConfigurationService(IAnimalConfigReader configReader, string configFileName = GameConstants.AnimalConfiguration) : IAnimalConfigurationService
    {
        private IEnumerable<AnimalConfig> _animalConfigs = configReader.GetAnimalConfiguration(configFileName);

        public AnimalConfig? GetConfig(string className)
        {
            return _animalConfigs.FirstOrDefault(c => c.Class == className);
        }
    }
}
