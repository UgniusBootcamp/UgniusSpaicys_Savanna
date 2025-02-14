using SavannaApp.Data.Interfaces.Game;

namespace GameOfLife.Data.Interfaces.Game
{
    public interface IMapCreator
    {
        /// <summary>
        /// Method to create map
        /// </summary>
        /// <returns>Map</returns>
        IMap CreateMap();
    }
}