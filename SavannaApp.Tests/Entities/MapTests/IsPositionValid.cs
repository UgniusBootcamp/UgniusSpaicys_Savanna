using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class IsPositionValid
    {
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            var animal1 = AnimalMock.CreateLion();
            var animal2 = AnimalMock.CreateLion(2, 1, 1);
            map.SetAnimal(animal1);
            map.SetAnimal(animal2);
        }

        [TestMethod]
        public void IsPositionValid_WithinBounds_ReturnsTrue()
        {
            //Arrange
            var expected = true;

            //Act
            var result = map.IsPositionValid(10, 10);

            //Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IsPositionValid_OutOfBounds_ReturnsFalse()
        {
            //Arrange
            var expected = false;

            //Act
            var result = map.IsPositionValid(21, 21);

            //Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IsPositionValid_NegativeCoordinates_ReturnsFalse()
        {
            //Arrange
            var expected = false;

            //Act
            var result = map.IsPositionValid(-1, -1);

            //Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IsPositionValid_AtAnimal_ReturnsFalse()
        {
            //Arrange
            var expected = false;

            //Act
            var result = map.IsPositionValid(0, 0);

            //Assert
            Assert.AreEqual(result, expected);
        }
    }
}
