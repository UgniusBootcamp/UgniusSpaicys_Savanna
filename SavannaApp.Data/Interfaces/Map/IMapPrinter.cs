namespace SavannaApp.Data.Interfaces.Game
{
    public interface IMapPrinter
    {
        /// <summary>
        /// Method to print map
        /// </summary>
        /// <param name="header">Header</param>
        /// <param name="map">Map</param>
        void PrintMap(string header, IMap map);
    }
}
