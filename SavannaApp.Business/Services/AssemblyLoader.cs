using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using System.Reflection;

namespace SavannaApp.Business.Services
{
    public class AssemblyLoader : IAssemblyLoader
    {
        private readonly string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Method for loading Animal Types from assembly
        /// </summary>
        /// <param name="directory">directory where assemblies are stored</param>
        /// <returns>types of animals</returns>
        /// <exception cref="Exception">if assembly fails to parse or load</exception>
        public List<Type> LoadAnimalTypes(string directory = GameConstants.PluginsDirectory)
        {
            var path = Path.Combine(baseDir, directory);
            var types = new List<Type>();

            if (!Directory.Exists(path)) return types;

            var dll = Directory.GetFiles(path, String.Format("*{0}", GameConstants.DllFileExtensions));

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
                    throw new Exception(String.Format("{0} {1}", GameConstants.DllLoadingFailed, e.Message));
                }
            }

            return types;
        }
    }
}
