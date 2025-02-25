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
        IMap _map = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = MapManagerMock.mapManager;
            _map = MapMock.CreateMap(10, 10);
        }

        [TestMethod]
        public void GetBestFreeSpaceForHunter_ClosestDistanceToAnimal_ShouldReturnClosestPosition()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2);
            var antelope = AnimalMock.CreateAntelope(2, 2, 6);

            _map.SetAnimal(lion);
            _map.SetAnimal(antelope);

            var expected = 1;

            //Act
            var result = _mapManager.GetBestFreeSpaceForHunter(lion, antelope, _map);

            //Assert
            Assert.AreEqual(expected, antelope.DistanceTo(result.X, result.Y));
        }

        [TestMethod]
        public void GetBestFreeSpaceForHunter_HunterInRangeToReachPray_ShouldNotReturnPositionAtAnimal()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2);
            var antelope = AnimalMock.CreateAntelope(1, 2, 5);

            _map.SetAnimal(lion);
            _map.SetAnimal(antelope);

            //Act
            var result = _mapManager.GetBestFreeSpaceForHunter(lion, antelope, _map);

            //Assert
            Assert.IsTrue(result.X != 2 && result.Y != 5);
        }
    }
}
