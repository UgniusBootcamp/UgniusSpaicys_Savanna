using System.Reflection;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class DecreaseHealth
    {
        private Animal animal = null!;

        [TestInitialize]
        public void Setup()
        {
            animal = AnimalMock.CreateLion();
        }

        [TestMethod]
        public void DecreaseHealth_ShouldReduceHealth()
        {
            // Arrange
            double initialHealth = animal.Features.Health;
            double damage = 3.0;

            var methodInfo = typeof(Animal).GetMethod("DecreaseHealth", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = methodInfo?.Invoke(animal, [damage]);

            // Assert
            Assert.AreEqual(initialHealth - damage, animal.Features.Health);
        }

        [TestMethod]
        public void DecreaseHealth_ShouldSetIsAliveToFalse_WhenHealthIsZeroOrLess()
        {
            // Arrange
            double damage = animal.Features.Health;
            var methodInfo = typeof(Animal).GetMethod("DecreaseHealth", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = methodInfo?.Invoke(animal, [damage]);

            // Assert
            Assert.IsFalse(animal.IsAlive);
        }
    }
}
