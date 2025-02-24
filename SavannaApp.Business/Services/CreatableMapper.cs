using System.Reflection;
using System.Runtime.Serialization;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class CreatableMapper : ICreatableMapper
    {
        public Dictionary<ConsoleKey, Type> MapCreatables(List<Type> types)
        {
            Dictionary<ConsoleKey, Type> creatableTypes = new Dictionary<ConsoleKey, Type>();

            try
            {
                foreach (var animalType in types)
                {
                    var creationKeyProp = animalType.GetProperty("CreationKey", BindingFlags.Public | BindingFlags.Instance);

                    if (creationKeyProp != null)
                    {
                        var animalInstance = Activator.CreateInstance(animalType, 0, 0, 0, new RandomMovement()) as Animal;

                        var creationKey = creationKeyProp.GetValue(animalInstance);

                        if (creationKey != null)
                        {
                            var key = (ConsoleKey)creationKey;

                            if(!creatableTypes.ContainsKey(key)) creatableTypes.Add(key, animalType);
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed mapping types into creatable objects {ex.Message}");
            }

            return creatableTypes;
        }
    }
}
