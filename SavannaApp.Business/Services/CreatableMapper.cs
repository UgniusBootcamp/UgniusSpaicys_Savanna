using System.Runtime.Serialization;
using SavannaApp.Business.Interfaces;
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
                foreach (var type in types)
                {
                    if (typeof(ICreatable).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var creationKeyProperty = type.GetProperty("CreationKey", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

                        if (creationKeyProperty != null && creationKeyProperty.PropertyType == typeof(ConsoleKey))
                        {
                            var key = (ConsoleKey)creationKeyProperty.GetValue(null);

                            if (!creatableTypes.ContainsKey(key))
                            {
                                creatableTypes[key] = type;
                            }
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
