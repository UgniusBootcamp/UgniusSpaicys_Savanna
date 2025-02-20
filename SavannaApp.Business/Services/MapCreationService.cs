using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class MapCreationService(IInputHandler inputHandler, IOutputHandler outputHandler) : IMapCreator
    {
        /// <summary>
        /// Map creation method
        /// </summary>
        /// <returns>Created map</returns>
        public IMap CreateMap()
        {
            int width = inputHandler.GetInt(String.Format("{0}{1}", GameConstants.LenghtInputMessage, GameConstants.DefaultMapLength));
            if (width <= 0 || width > GameConstants.DefaultMapLength) width = GameConstants.DefaultMapLength;

            int height = inputHandler.GetInt(String.Format("{0}{1}", GameConstants.HeightInputMessage, GameConstants.DefaultMapHeight));
            if (height <= 0 || height > GameConstants.DefaultMapHeight) height = GameConstants.DefaultMapHeight;

            outputHandler.Clear();

            return new Map(height, width);
        }
    }
}