using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class GetAnimal
    {
        private IMap map = null!;
        private Animal animal = null!;

        [TestInitialize]
        public void Setup()
        {
            animal = AnimalMock.CreateLion(1, 5, 5);
            map = new Map(20, 20);
        }

        [TestMethod]
        public void GetAnimal_ReturnsCorrectAnimal()
        {
            // Arrange
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
