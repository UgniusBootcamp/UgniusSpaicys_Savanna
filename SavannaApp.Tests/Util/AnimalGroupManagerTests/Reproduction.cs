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
        private Mock<IAnimalCreationService> _animalCreationServiceMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _animalCreationServiceMock = new Mock<IAnimalCreationService>();
            _manager = new AnimalGroupManager(_animalCreationServiceMock.Object);
        }

        [TestMethod]
        public void Reproduction_ShouldCallRebirthAnimal_AfterRequiredRounds()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 1, 2);
            var lion3 = AnimalMock.CreateLion(3, 10, 10);
            var lion4 = AnimalMock.CreateLion(4, 10, 11);

            mapMock.Setup(m => m.Animals).Returns(new List<Animal> { lion1, lion2, lion3, lion4 });

            //Act
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Exactly(2));
        }


        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterRequiredRounds()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 5, 5);
            var lion3 = AnimalMock.CreateLion(3, 15, 15);
            var lion4 = AnimalMock.CreateLion(4, 10, 11);

            mapMock.Setup(m => m.Animals).Returns(new List<Animal> { lion1, lion2, lion3, lion4 });

            //Act
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }


        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterAnimalMoves()
        {
            // Arrange
            var mapMock = new Mock<IMap>();

            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 2, 1);

            mapMock.Setup(m => m.Animals).Returns(new List<Animal> { lion1, lion2 });

            //Act
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);
            lion2.Position.X = 3;
            _manager.Reproduction(mapMock.Object);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }

        [TestMethod]
        public void Reproduction_ShouldNotCallRebirthAnimal_AfterAnimaComeback()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var mapMockAfterMove = new Mock<IMap>();

            var lion1 = AnimalMock.CreateLion(1, 1, 1);
            var lion2 = AnimalMock.CreateLion(2, 2, 1);

            mapMock.Setup(m => m.Animals).Returns(new List<Animal> { lion1, lion2 });

            //Act
            _manager.Reproduction(mapMock.Object);
            _manager.Reproduction(mapMock.Object);
            lion2.Position.X = 3;
            _manager.Reproduction(mapMock.Object);
            lion2.Position.X = 2;
            _manager.Reproduction(mapMock.Object);

            // Assert
            _animalCreationServiceMock.Verify(a => a.RebirthAnimal(It.IsAny<List<Animal>>(), It.IsAny<IMap>()), Times.Never);
        }

    }
}
