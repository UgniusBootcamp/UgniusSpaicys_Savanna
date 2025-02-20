using Moq;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapManagerTests
{
    [TestClass]
    public class GetRandomFreePlaceOnMap
    {
        private IMapManager _mapManager = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = new MapManager();
        }

        [TestMethod]
        public void GetRandomFreePlaceOnMap_MapIsFull_ReturnNull()
        {
            //Arrange
            Mock<IMap> mock = new Mock<IMap>();
            var height = 2;
            var width = 2;

            mock.Setup(m => m.Width).Returns(width);
            mock.Setup(m => m.Height).Returns(height);

            mock.Setup(m => m.Animals).Returns(new List<Animal>
            {
                AnimalMock.CreateLion(1,0,0),
                AnimalMock.CreateLion(2,0,1),
                AnimalMock.CreateLion(3,1,0),
                AnimalMock.CreateLion(4,1,1),
            });

            //Act 

            var result = _mapManager.GetRandomFreePlaceOnMap(mock.Object);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRandomFreePlaceOnMap_MapHasMultipleFreeSpaces_ReturnRandomFreeSpace()
        {
            //Arrange
            Mock<IMap> mock = new Mock<IMap>();
            var height = 2;
            var width = 2;

            var expectedX = 1;
            var expectedY = 1;

            mock.Setup(m => m.Width).Returns(width);
            mock.Setup(m => m.Height).Returns(height);

            mock.Setup(m => m.Animals).Returns(new List<Animal>
            {
                AnimalMock.CreateLion(1,0,0),
                AnimalMock.CreateLion(2,0,1),
                AnimalMock.CreateLion(3,1,0),
            });

            mock.Setup(m => m.IsPositionValid(It.IsInRange(0, 1, Moq.Range.Inclusive), It.IsInRange(0, 1, Moq.Range.Inclusive))).Returns((int x, int y) => x == 1 && y == 1);

            //Act 
            var result = _mapManager.GetRandomFreePlaceOnMap(mock.Object);

            //Assert
            Assert.IsTrue(result.X == expectedX && result.Y == expectedY);

        }
    }
}
