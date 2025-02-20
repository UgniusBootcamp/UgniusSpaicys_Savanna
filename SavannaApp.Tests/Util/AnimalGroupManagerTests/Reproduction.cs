using SavannaApp.Data.Interfaces;
using Moq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.AnimalGroupManagerTests
{
    [TestClass]
    public class Reproduction
    {
        private IAnimalGroupManager _manager = null!;
        private IMap _map = null!;
        private Mock<IAnimalCreationService> _animalCreationServiceMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _animalCreationServiceMock = new Mock<IAnimalCreationService>();
            _manager = AnimalGroupManagerMock.CreateAnimalGroupManager(_animalCreationServiceMock.Object);
            _map = MapMock.CreateMap(20, 20);
        }

        [TestMethod]
        public void Reproduction_ShouldCallRebirthAnimal_AfterRequiredRounds()
        {
            // Arrange
            _map.SetAnimal(AnimalMock.CreateLion(1, 1, 1));
            _map.SetAnimal(AnimalMock.CreateLion(2, 1, 2));
            _map.SetAnimal(AnimalMock.CreateLion(3, 10, 10));
            _map.SetAnimal(AnimalMock.CreateLion(4, 10, 11));

            //Act
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Exactly(2));
        }


        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterRequiredRounds()
        {
            // Arrange
            _map.SetAnimal(AnimalMock.CreateLion(1, 1, 1));
            _map.SetAnimal(AnimalMock.CreateLion(2, 5, 5));
            _map.SetAnimal(AnimalMock.CreateLion(3, 15, 15));
            _map.SetAnimal(AnimalMock.CreateLion(4, 10, 11));


            //Act
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }


        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterAnimalMoves()
        {
            // Arrange
            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 2, 1);

            _map.SetAnimal(lion1);
            _map.SetAnimal(lion2);

            //Act
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);
            lion2.Position.X = 3;
            _manager.Reproduction(_map);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterAnimaComeback()
        {
            // Arrange

            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 2, 1);

            _map.SetAnimal(lion1);
            _map.SetAnimal(lion2);

            //Act
            _manager.Reproduction(_map);
            _manager.Reproduction(_map);
            lion2.Position.X = 3;
            _manager.Reproduction(_map);
            lion2.Position.X = 2;
            _manager.Reproduction(_map);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _map.Animals.Clear();
        }

    }
}
