using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IMapManager
    {
        /// <summary>
        /// Method to get random free place on map
        /// </summary>
        /// <param name="map">random free place on map</param>
        /// <returns>null if map is full, free place otherwise</returns>
        Position? GetRandomFreePlaceOnMap(IMap map);

        /// <summary>
        /// Method to get best possible position for hunter based on pray
        /// </summary>
        /// <param name="hunter">hunter</param>
        /// <param name="pray">pray</param>
        /// <param name="map">map</param>
        /// <returns>best possible position for hunter based on pray</returns>
        Position GetBestFreeSpaceForHunter(Animal hunter, Animal pray, IMap map);

        /// <summary>
        /// Method to get best free space for pray based on animals surrounding him
        /// </summary>
        /// <param name="pray">pray</param>
        /// <param name="hunters">hunters</param>
        /// <param name="map">map</param>
        /// <returns>best possible position based on animals surrounding pray</returns>
        Position GetBestFreeSpaceForPray(Animal pray, IEnumerable<Animal> hunters, IMap map);

        /// <summary>
        /// Method to get closest position to animals
        /// </summary>
        /// <param name="animals">animals</param>
        /// <param name="map">map</param>
        /// <returns>closest position to animals</returns>
        Position? GetClosestFreePositionToAnimals(List<Animal> animals, IMap map);
    }
}
