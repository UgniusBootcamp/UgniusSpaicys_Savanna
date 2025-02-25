using SavannaApp.Business.Interfaces;

namespace SavannaApp.Business.Services
{
    public class CreatableMapper(IAnimalConfigurationService configurationService) : ICreatableMapper
    {
        /// <summary>
        /// Method to map types with console keys
        /// </summary>
        /// <param name="types">class types</param>
        /// <returns>map with console key and type</returns>
        /// <exception cref="Exception">if mapping fails</exception>
        public Dictionary<ConsoleKey, Type> MapCreatableAnimals(List<Type> types)
        {
            Dictionary<ConsoleKey, Type> creatableTypes = new Dictionary<ConsoleKey, Type>();

            foreach (var type in types) 
            {
                var animalConfig = configurationService.GetConfig(type.Name);

                if (animalConfig == null) continue;

                var key = (ConsoleKey)animalConfig.Key;
                if (!creatableTypes.ContainsKey(key)) creatableTypes.Add(key, type);
            }

            return creatableTypes;
        }
    }
}
