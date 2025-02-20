using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Interfaces
{
    public interface IAnimalGroupManager
    {
        /// <summary>
        /// Method for animal reproduction
        /// </summary>
        /// <param name="map">map</param>
        void Reproduction(IMap map);
    }
}
