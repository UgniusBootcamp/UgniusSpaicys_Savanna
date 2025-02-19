using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IMapManager
    {
        Position? GetRandomFreePlaceOnMap(IMap map);
        Position GetBestFreeSpaceForHunter(Animal hunter, Animal pray, IMap map);
        Position GetBestFreeSpaceForPray(Animal pray, IEnumerable<Animal> hunters, IMap map);
        Position? GetClosestFreePositionToAnimals(List<Animal> animals, IMap map);
    }
}
