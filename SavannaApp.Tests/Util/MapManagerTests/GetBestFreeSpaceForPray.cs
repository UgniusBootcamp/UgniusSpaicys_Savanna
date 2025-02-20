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
        IMap _map = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = MapManagerMock.mapManager;
            _map = MapMock.CreateMap(10, 10);
        }

        [TestMethod]
        public void GetBestFreeSpaceForPray_ShouldMoveAwaysFromHunters_ShouldReturnFurtherDistance()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2, "L", 3, 5);
            var lion2 = AnimalMock.CreateLion(2, 3, 3, "L", 3, 5);
            var hunters = new[] { lion, lion2 };
            var antelope = AnimalMock.CreateLion(3, 2, 3, "L", 3, 5);

            _map.SetAnimal(lion);
            _map.SetAnimal(lion2);
            _map.SetAnimal(antelope);

            var currentDistance = hunters.Sum(h => h.DistanceTo(antelope.Position.X, antelope.Position.Y));

            //Act
            var result = _mapManager.GetBestFreeSpaceForPray(antelope, hunters, _map);

            //Assert
            Assert.IsTrue(currentDistance < antelope.DistanceTo(result.X, result.Y));
        }
    }
}
