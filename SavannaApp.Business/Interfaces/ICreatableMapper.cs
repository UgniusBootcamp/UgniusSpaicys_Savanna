namespace SavannaApp.Business.Interfaces
{
    public interface ICreatableMapper
    {
        /// <summary>
        /// Method to map types with console keys
        /// </summary>
        /// <param name="types">class types</param>
        /// <returns>map with console key and type</returns>
        Dictionary<ConsoleKey, Type> MapCreatableAnimals(List<Type> types);
    }
}
