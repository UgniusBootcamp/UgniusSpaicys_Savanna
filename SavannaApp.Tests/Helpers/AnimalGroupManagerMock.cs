using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AnimalGroupManagerMock
    {
        public static AnimalGroupManager CreateAnimalGroupManager(IAnimalCreationService animalCreationService)
        {
            return new AnimalGroupManager(animalCreationService);
        }
    }
}
