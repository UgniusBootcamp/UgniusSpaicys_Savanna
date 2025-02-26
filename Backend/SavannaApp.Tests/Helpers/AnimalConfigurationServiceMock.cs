using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalConfigurationServiceMock
    {
        public static IAnimalConfigurationService AnimalConfigurationService = new AnimalConfigurationService(AnimalConfigReaderMock.AnimalConfigReader);
    }
}
