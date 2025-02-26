using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Interfaces
{
    public interface IMapPrinter
    {
        /// <summary>
        /// Method to print map
        /// </summary>
        /// <param name="header">Header</param>
        /// <param name="map">Map</param>
        void PrintMap(string header, string footer, IMap map);
    }
}
