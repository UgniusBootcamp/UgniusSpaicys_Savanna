using Moq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapManagerTests
{
    [TestClass]
    public class GetClosestFreePositionToAnimals
    {
        IMapManager _mapManager = null!;
        Mock<IMap> _map = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = new MapManager();
            _map = new Mock<IMap>();
        }

        [TestMethod]
        public void GetClosestFreePositionToAnimals_GetPositionBetweenTwoAnimals_ShouldReturnPositionBetweenAnimals()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2, "L");
            var lion2 = AnimalMock.CreateLion(1, 4, 4, "L");
            var animals = new List<Animal> { lion, lion2 };
            var mapHeight = 10;
            var mapWidth = 10;

            _map.Setup(m => m.Width).Returns(mapWidth);
            _map.Setup(m => m.Height).Returns(mapHeight);

            _map.Setup(m => m.IsPositionValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _map.Setup(m => m.IsPositionValid(2, 2)).Returns(false);
            _map.Setup(m => m.IsPositionValid(4, 4)).Returns(false);

            var expectedX = 3;
            var expectedY = 3;
            //Act
            var result = _mapManager.GetClosestFreePositionToAnimals(animals, _map.Object);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedX, result.X);
            Assert.AreEqual(expectedY, result.Y);
        }
    }
}
