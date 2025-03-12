using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalConfigReaderMock
    {
        public static IAnimalConfigReader AnimalConfigReader = new JsonAnimalConfigurationReader();
    }
}
