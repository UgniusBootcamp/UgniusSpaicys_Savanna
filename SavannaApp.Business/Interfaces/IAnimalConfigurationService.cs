using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalConfigurationService
    {
        /// <summary>
        /// Method to get configuration from class name
        /// </summary>
        /// <param name="className">name of class</param>
        /// <returns>Animal configuration</returns>
        AnimalConfig? GetConfig(string className);
    }
}
