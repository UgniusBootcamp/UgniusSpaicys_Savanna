using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces
{
    public interface IMovement
    {
        /// <summary>
        /// Strategy how to move
        /// </summary>
        /// <param name="animal">animal to move</param>
        /// <param name="map">map</param>
        /// <returns>true of aninmal has moved, false otherwise</returns>
        bool Move(Animal animal, IMap map);
    }
}
