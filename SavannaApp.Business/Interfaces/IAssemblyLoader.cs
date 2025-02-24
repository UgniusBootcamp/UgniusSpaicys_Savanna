namespace SavannaApp.Business.Interfaces
{
    public interface IAssemblyLoader
    {
        public List<Type> LoadAnimalTypes(string directory = "Plugins");
    }
}
