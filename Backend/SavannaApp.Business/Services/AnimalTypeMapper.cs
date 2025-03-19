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

        public Type? GetType(int id) 
        {
            if (AnimalTypesById.ContainsKey(id)) return AnimalTypesById[id];

            return null;
        }

        public IEnumerable<AnimalTypeReadDto> MapAnimalTypes()
        {
            return AnimalTypesById.Select(a => new AnimalTypeReadDto
            {
                Id = a.Key,
                AnimalType = a.Value.Name
            });
        }
    }
}
