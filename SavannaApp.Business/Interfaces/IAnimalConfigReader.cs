using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalConfigReader
    {
        /// <summary>
        /// Method for loading animla configuration
        /// </summary>
        /// <param name="fileName">config file name</param>
        /// <returns>Animal configuration</returns>
        public IEnumerable<AnimalConfig> GetAnimalConfiguration(string fileName);
    }
}
