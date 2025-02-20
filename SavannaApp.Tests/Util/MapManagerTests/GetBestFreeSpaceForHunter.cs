using Moq;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapManagerTests
{
    [TestClass]
    public class GetBestFreeSpaceForHunter
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
        public void GetBestFreeSpaceForHunter_ClosestDistanceToAnimal_ShouldReturnClosestPosition()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2, "L", 3, 5);
            var antelope = AnimalMock.CreateAntelope(2, 2, 6, "A", 3, 5);
            var mapHeight = 10;
            var mapWidth = 10;

            _map.Setup(m => m.Width).Returns(mapWidth);
            _map.Setup(m => m.Height).Returns(mapHeight);

            _map.Setup(m => m.IsPositionValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _map.Setup(m => m.IsPositionValid(2, 2)).Returns(false);
            _map.Setup(m => m.IsPositionValid(2, 6)).Returns(false);

            var expected = 1;

            //Act
            var result = _mapManager.GetBestFreeSpaceForHunter(lion, antelope, _map.Object);

            //Assert
            Assert.AreEqual(expected, antelope.DistanceTo(result.X, result.Y));
        }

        [TestMethod]
        public void GetBestFreeSpaceForHunter_HunterInRangeToReachPray_ShouldNotReturnPositionAtAnimal()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2, "L", 3, 5);
            var antelope = AnimalMock.CreateAntelope(1, 2, 5, "A", 3, 5);
            var mapHeight = 10;
            var mapWidth = 10;

            _map.Setup(m => m.Width).Returns(mapWidth);
            _map.Setup(m => m.Height).Returns(mapHeight);

            _map.Setup(m => m.IsPositionValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _map.Setup(m => m.IsPositionValid(2, 2)).Returns(false);
            _map.Setup(m => m.IsPositionValid(2, 5)).Returns(false);

            //Act
            var result = _mapManager.GetBestFreeSpaceForHunter(lion, antelope, _map.Object);

            //Assert
            Assert.IsTrue(result.X != 2 && result.Y != 5);
        }
    }
}
