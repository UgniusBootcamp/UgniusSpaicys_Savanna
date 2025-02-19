using SavannaApp.Data.Interfaces;
using Moq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Util.AnimalGroupManagerTests
{
    [TestClass]
    public class Reproduction
    {
        private IAnimalGroupManager _manager = null!;
        private Mock<IAnimalCreationService> _animalCreationServiceMock = null!;
        private Mock<IMovement> _movementServiceMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _animalCreationServiceMock = new Mock<IAnimalCreationService>();
            _manager = new AnimalGroupManager(_animalCreationServiceMock.Object);
            _movementServiceMock = new Mock<IMovement>();
        }

        [TestMethod]
        public void Reproduction_ShouldCallRebirthAnimal_AfterRequiredRounds()
        {
            // Arrange
            var mapMock = new Mock<IMap>();
            var lion1 = new Lion(1, 1, 1, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion2 = new Lion(2, 1, 2, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion3 = new Lion(3, 10, 10, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion4 = new Lion(4, 10, 11, "L", 1, 1, 1, _movementServiceMock.Object);

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
            var lion1 = new Lion(1, 1, 1, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion2 = new Lion(2, 5, 5, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion3 = new Lion(3, 15, 15, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion4 = new Lion(4, 10, 11, "L", 1, 1, 1, _movementServiceMock.Object);

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

            var lion1 = new Lion(1, 1, 1, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion2 = new Lion(2, 2, 1, "L", 1, 1, 1, _movementServiceMock.Object);


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

            var lion1 = new Lion(1, 1, 1, "L", 1, 1, 1, _movementServiceMock.Object);
            var lion2 = new Lion(2, 2, 1, "L", 1, 1, 1, _movementServiceMock.Object);

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
