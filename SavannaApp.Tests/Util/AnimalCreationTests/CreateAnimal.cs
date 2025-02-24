using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;
using SavannaApp.Business.Interfaces;
using SavannaApp.Animals;

namespace SavannaApp.Tests.Util.AnimalCreationTests
{
    [TestClass]
    public class CreateAnimal
    {
        private IAnimalCreationService _animalCreationService = null!;

        [TestInitialize]
        public void Setup()
        {
            _animalCreationService = AnimalCreationMock.AnimalCreationService;
        }

        [TestMethod]
        public void CreateAnimal_NoSpaceLeft_ShouldReturnNull()
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
        public void CreateAnimal_CreateAnimal_ShouldReturnAntelope()
        {
            //Arrange
            IMap map = MapMock.CreateMap(3, 3);

            //Act
            var result = _animalCreationService.CreateAnimal(typeof(Antelope), map);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Antelope));
        }
    }
}
