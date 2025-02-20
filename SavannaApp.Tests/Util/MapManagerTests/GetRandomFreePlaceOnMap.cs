using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapManagerTests
{
    [TestClass]
    public class GetRandomFreePlaceOnMap
    {
        private IMapManager _mapManager = null!;
        private IMap _map = null!;

        [TestInitialize]
        public void Setup()
        {
            _map = MapMock.CreateMap(2, 2);
            _mapManager = MapManagerMock.mapManager;
        }

        [TestMethod]
        public void GetRandomFreePlaceOnMap_MapIsFull_ReturnNull()
        {
            //Arrange
            _map.SetAnimal(AnimalMock.CreateLion(1, 0, 0));
            _map.SetAnimal(AnimalMock.CreateLion(2, 0, 1));
            _map.SetAnimal(AnimalMock.CreateLion(3, 1, 0));
            _map.SetAnimal(AnimalMock.CreateLion(4, 1, 1));

            //Act 

            var result = _mapManager.GetRandomFreePlaceOnMap(_map);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRandomFreePlaceOnMap_MapHasMultipleFreeSpaces_ReturnRandomFreeSpace()
        {
            //Arrange

            var expectedX = 1;
            var expectedY = 1;

            _map.SetAnimal(AnimalMock.CreateLion(1, 0, 0));
            _map.SetAnimal(AnimalMock.CreateLion(2, 0, 1));
            _map.SetAnimal(AnimalMock.CreateLion(3, 1, 0));

            //Act 
            var result = _mapManager.GetRandomFreePlaceOnMap(_map);

            //Assert
            Assert.IsTrue(result.X == expectedX && result.Y == expectedY);
        }
    }
}
