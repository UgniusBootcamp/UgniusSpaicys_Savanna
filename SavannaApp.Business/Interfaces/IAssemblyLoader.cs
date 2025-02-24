using SavannaApp.Data.Constants;

namespace SavannaApp.Business.Interfaces
{
    public interface IAssemblyLoader
    {
        /// <summary>
        /// Method for loading Animal Types from assembly
        /// </summary>
        /// <param name="directory">directory where assemblies are stored</param>
        /// <returns>types of animals</returns>
        public List<Type> LoadAnimalTypes(string directory = GameConstants.PluginsDirectory);
    }
}
