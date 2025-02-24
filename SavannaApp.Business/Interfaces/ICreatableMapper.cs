namespace SavannaApp.Business.Interfaces
{
    public interface ICreatableMapper
    {
        Dictionary<ConsoleKey, Type> MapCreatables(List<Type> types);
    }
}
