using System.Text.Json;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Helpers.Configuration;

namespace SavannaApp.Business.Services
{
    public class JsonAnimalConfigurationReader : IAnimalConfigReader
    {
        private string _baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GameConstants.ConfigDirectory) + "\\";
        public IEnumerable<AnimalConfig> GetAnimalConfiguration(string fileName)
        {
            try
            {
                fileName = String.Format("{0}{1}", _baseDir, fileName);

                if (!File.Exists(fileName)) throw new FileNotFoundException(String.Format(GameConstants.FileNotFound, fileName));

                string json = File.ReadAllText(fileName);
                var result = JsonSerializer.Deserialize<List<AnimalConfig>>(json);

                return result ?? [];
            }
            catch (Exception e)
            {
                throw new Exception(String.Format(GameConstants.ConfigLoadFail, e.Message));
            }
        }
    }
}
