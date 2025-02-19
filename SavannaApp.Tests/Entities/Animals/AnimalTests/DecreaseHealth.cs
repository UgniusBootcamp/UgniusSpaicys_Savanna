using System.Reflection;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.MovementStrategies;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class DecreaseHealth
    {
        [TestMethod]
        public void DecreaseHealth_ShouldReduceHealth()
        {
            // Arrange
            Antelope animal = new Antelope(2, 10, 12, "A", 5, 5, 5, new RandomMovement());
            double initialHealth = animal.Health;
            double damage = 3.0;

            var methodInfo = typeof(Animal).GetMethod("DecreaseHealth", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = methodInfo?.Invoke(animal, [damage]);

            // Assert
            Assert.AreEqual(initialHealth - damage, animal.Health);
        }

        [TestMethod]
        public void DecreaseHealth_ShouldSetIsAliveToFalse_WhenHealthIsZeroOrLess()
        {
            // Arrange
            Antelope animal = new Antelope(2, 10, 12, "A", 5, 5, 5, new RandomMovement());
            double damage = animal.Health;
            var methodInfo = typeof(Animal).GetMethod("DecreaseHealth", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            var result = methodInfo?.Invoke(animal, [damage]);

            // Assert
            Assert.IsFalse(animal.IsAlive);
        }
    }
}
