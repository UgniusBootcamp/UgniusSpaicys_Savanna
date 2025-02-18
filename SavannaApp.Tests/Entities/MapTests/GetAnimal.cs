using SavannaApp.Data.Entities;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Entities.Animals;
using Moq;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class GetAnimal
    {
        private IMap map = null!;
        private Mock<IMovement> random = null!;

        [TestInitialize]
        public void Setup()
        {
            random = new Mock<IMovement>();
            map = new Map(20, 20);
        }

        [TestMethod]
        public void GetAnimal_ReturnsCorrectAnimal()
        {
            // Arrange
            var animal = new Lion(1, 5, 5, "Lion", 10, 5, 100, random.Object);
            map.SetAnimal(animal);

            // Act
            var result = map.GetAnimal(5, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(animal, result);
        }

        [TestMethod]
        public void GetAnimal_ReturnsNullIfNoAnimal()
        {
            // Act
            var result = map.GetAnimal(0, 0);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAnimal_ReturnsNullIfInvalidPosition()
        {
            // Act and Assert
            map.GetAnimal(-1, -1);
        }
    }
}
