using System.Reflection;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class CreatableMapper : ICreatableMapper
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

            try
            {
                foreach (var animalType in types)
                {
                    var creationKeyProp = animalType.GetProperty("CreationKey", BindingFlags.Public | BindingFlags.Instance);

                    if (creationKeyProp != null)
                    {
                        var constructor = animalType.GetConstructor([typeof(int), typeof(int), typeof(int), typeof(IMovement)]);

                        if (constructor != null)
                        {
                            var movement = new RandomMovement();
                            var animalInstance = constructor.Invoke([1, 1, 1, movement]) as Animal;

                            var creationKey = creationKeyProp.GetValue(animalInstance);

                            if (creationKey != null)
                            {
                                var key = (ConsoleKey)creationKey;

                                if (!creatableTypes.ContainsKey(key)) creatableTypes.Add(key, animalType);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} {1}", GameConstants.MappingFails, ex.Message));
            }

            return creatableTypes;
        }
    }
}
