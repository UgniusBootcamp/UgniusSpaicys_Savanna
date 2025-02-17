using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class IsPositionValid
    {
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            IMovement random = new RandomMovement();
            map = new Map(20, 20);
            var animal1 = new Lion(1, 0, 0, "L", 1, 1, 1, random);
            var animal2 = new Lion(1, 1, 1, "L", 1, 1, 1, random);
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
