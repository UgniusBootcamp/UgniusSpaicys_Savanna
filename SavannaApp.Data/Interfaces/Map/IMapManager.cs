using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Interfaces.Map
{
    public interface IMapManager
    {
        Position? GetRandomFreePlaceOnMap(IMap map);
        Position GetBestFreeSpaceForHunter(Animal hunter, Animal pray, IMap map);
        Position GetBestFreeSpaceForPray(Animal pray, IEnumerable<Animal> hunters, IMap map);
        Position? GetClosestFreePositionToAnimals(List<Animal> animals, IMap map);
    }
}
