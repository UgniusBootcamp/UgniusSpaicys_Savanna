using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Dto.Game;

namespace SavannaApp.Business.Services
{
    public class AnimalTypeMapper
    {
        public Dictionary<int, Type> AnimalTypesById = new Dictionary<int, Type>();

        public AnimalTypeMapper(IAssemblyLoader assemblyLoader)
        {
            var types = assemblyLoader.LoadAnimalTypes();

            for(int i = 1; i <= types.Count(); i++)
            {
                AnimalTypesById[i] = types[i - 1];
            }
        }

        /// <summary>
        /// Get animal type by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>animal type</returns>
        public Type? GetType(int id) 
        {
            if (AnimalTypesById.ContainsKey(id)) return AnimalTypesById[id];

            return null;
        }

        /// <summary>
        /// Get animal types with ids
        /// </summary>
        /// <returns>animal types with ids</returns>
        public IEnumerable<AnimalTypeReadDto> MapAnimalTypes()
        {
            return AnimalTypesById.Select(a => new AnimalTypeReadDto
            {
                Id = a.Key,
                AnimalType = a.Value.Name
            });
        }

        /// <summary>
        /// Get animal id by its type
        /// </summary>
        /// <param name="type">animal type</param>
        /// <returns>animal type id</returns>
        public int? GetAnimalId(Type type)
        {
            return AnimalTypesById.FirstOrDefault(kvp => kvp.Value == type).Key;
        }
    }
}
