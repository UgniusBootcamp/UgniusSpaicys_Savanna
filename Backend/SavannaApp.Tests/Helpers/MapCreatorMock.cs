using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class MapCreatorMock
    {
        public static IMapCreator CreateMapCreator(IOutputHandler outputHandler, IInputHandler inputHandler)
        {
            return new MapCreationService(inputHandler, outputHandler);
        }
    }
}
