using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalFactoryMock
    {
        public static IAnimalFactory Factory = new AnimalFactory(MovementMock.Hunter, MovementMock.Pray, AnimalConfigurationServiceMock.AnimalConfigurationService);
    }
}
