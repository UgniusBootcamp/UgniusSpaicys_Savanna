using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using System.Reflection;

namespace SavannaApp.Business.Services
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        public List<Type> LoadAnimalTypes(string directory = "Plugins")
        {
            var path = Path.Combine(baseDir, directory);
            var types = new List<Type>();

            if (!Directory.Exists(path)) return types;

            var dll = Directory.GetFiles(path, "*.dll");

            foreach (var dllFile in dll)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);

                    var animalTypes = assembly.GetTypes()
                        .Where(a => a.IsSubclassOf(typeof(Animal))).ToList();

                    types.AddRange(animalTypes);
                }
                catch (Exception e)
                {
                    throw new Exception($"System has failed loading assembly {e.Message}");
                }
            }

            return types;
        }
    }
}
