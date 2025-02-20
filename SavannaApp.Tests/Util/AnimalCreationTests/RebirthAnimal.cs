using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Tests.Helpers;
using SavannaApp.Business.Interfaces;

namespace SavannaApp.Tests.Util.AnimalCreationTests
{
    [TestClass]
    public class RebirthAnimal
    {
        private IAnimalCreationService _animalCreationService = null!;

        [TestInitialize]
        public void Setup()
        {
            _animalCreationService = AnimalCreationMock.AnimalCreationService;
        }

        [TestMethod]
        public void RebirthAnimal_NoSpaceLeft_ShouldReturnNull()
        {
            //Arrange
            IMap map = MapMock.CreateMap(1, 1);
            map.SetAnimal(AnimalMock.CreateAntelope());

            //Act
            var result = _animalCreationService.CreateAnimal(typeof(Antelope), map);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RebirthAnimal_SpaceBetweenAnimals_ShouldReturnMiddlePosition()
        {
            //Arrange
            IMap map = MapMock.CreateMap(10, 10);
            map.SetAnimal(AnimalMock.CreateAntelope());
            map.SetAnimal(AnimalMock.CreateAntelope(2, 2, 2));

            var expectedX = 1;
            var expectedY = 1;

            //Act
            var result = _animalCreationService.RebirthAnimal(map.Animals, map);

            //Assert
            Assert.AreEqual(result.Position.X, expectedX);
            Assert.AreEqual(result.Position.Y, expectedY);
        }
    }
}
