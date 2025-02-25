using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalConfigurationService
    {
        AnimalConfig? GetConfig(string className);
    }
}
