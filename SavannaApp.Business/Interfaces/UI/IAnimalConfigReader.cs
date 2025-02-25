using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Interfaces.UI
{
    public interface IAnimalConfigReader
    {
        public IEnumerable<AnimalConfig> GetAnimalConfiguration(string fileName);
    }
}
