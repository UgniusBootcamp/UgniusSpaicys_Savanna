using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class DistanceTo
    {
        private Animal animal = null!;
        private IMovement? random;

        [TestInitialize]
        public void Setup()
        {
            random = new RandomMovement();
            animal = new Lion(1, 2, 2, "L", 1, 1, 1, random);
        }

        [TestMethod]
        public void DistanceTo_Diagonal_ReturnCorrectResult()
        {
            //Arrange
            var x = 0; var y = 0;
            var expected = 2;

            //Act
            var result = animal.DistanceTo(x, y);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DistanceTo_Vertical_ReturnCorrectResult()
        {
            //Arrange
            var x = 0; var y = 2;
            var expected = 2;

            //Act
            var result = animal.DistanceTo(x, y);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DistanceTo_SameSpot_ReturnCorrectResult()
        {
            //Arrange
            var x = 2; var y = 2;
            var expected = 0;

            //Act
            var result = animal.DistanceTo(x, y);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DistanceTo_Horizontal_ReturnCorrectResult()
        {
            //Arrange
            var x = 2; var y = 0;
            var expected = 2;

            //Act
            var result = animal.DistanceTo(x, y);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DistanceTo_NegativeCoordinates_ReturnCorrectResult()
        {
            //Arrange
            var x = -2; var y = -2;
            var expected = 4;

            //Act
            var result = animal.DistanceTo(x, y);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
