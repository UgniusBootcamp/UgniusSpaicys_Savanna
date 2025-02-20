using Moq;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapManagerTests
{
    [TestClass]
    public class GetBestFreeSpaceForPray
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
        public void GetBestFreeSpaceForPray_ShouldMoveAwaysFromHunters_ShouldReturnFurtherDistance()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2, "L", 3, 5);
            var lion2 = AnimalMock.CreateLion(2, 3, 3, "L", 3, 5);
            var hunters = new[] { lion, lion2 };
            var antelope = AnimalMock.CreateLion(3, 2, 3, "L", 3, 5);
            var mapHeight = 10;
            var mapWidth = 10;

            _map.Setup(m => m.Width).Returns(mapWidth);
            _map.Setup(m => m.Height).Returns(mapHeight);

            _map.Setup(m => m.IsPositionValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _map.Setup(m => m.IsPositionValid(2, 2)).Returns(false);
            _map.Setup(m => m.IsPositionValid(2, 3)).Returns(false);
            _map.Setup(m => m.IsPositionValid(3, 3)).Returns(false);

            var currentDistance = hunters.Sum(h => h.DistanceTo(antelope.Position.X, antelope.Position.Y));

            //Act
            var result = _mapManager.GetBestFreeSpaceForPray(antelope, hunters, _map.Object);

            //Assert
            Assert.IsTrue(currentDistance < antelope.DistanceTo(result.X, result.Y));
        }
    }
}
