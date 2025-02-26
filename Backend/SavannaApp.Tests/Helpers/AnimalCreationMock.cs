using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalCreationMock
    {
        public static IAnimalCreationService AnimalCreationService  = new AnimalCreationService(AnimalFactoryMock.Factory, MapManagerMock.mapManager);
    }
}
