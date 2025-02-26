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
        IMap _map = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapManager = MapManagerMock.mapManager;
            _map = MapMock.CreateMap(10, 10);
        }

        [TestMethod]
        public void GetClosestFreePositionToAnimals_GetPositionBetweenTwoAnimals_ShouldReturnPositionBetweenAnimals()
        {
            //Arrange
            var lion = AnimalMock.CreateLion(1, 2, 2);
            var lion2 = AnimalMock.CreateLion(1, 4, 4);
            var animals = new List<Animal> { lion, lion2 };
            _map.SetAnimal(lion);
            _map.SetAnimal(lion2);

            var expectedX = 3;
            var expectedY = 3;
            //Act
            var result = _mapManager.GetClosestFreePositionToAnimals(animals, _map);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedX, result.X);
            Assert.AreEqual(expectedY, result.Y);
        }
    }
}
