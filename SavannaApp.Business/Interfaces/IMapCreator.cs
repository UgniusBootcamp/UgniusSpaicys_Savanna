using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Interfaces
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