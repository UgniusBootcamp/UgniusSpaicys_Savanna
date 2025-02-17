using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class IncreaseHealth
    {
        private Animal animal = null!;

        [TestInitialize]
        public void Setup()
        {
            animal = new Antelope(2, 10, 12, "A", 5, 5, 5, new RandomMovement());
        }

        [TestMethod]
        public void IncreaseHealth_PositiveValue_IncreasesHealth()
        {
            // Arrange
            double initialHealth = animal.Health;
            double increaseAmount = 5.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth + increaseAmount, animal.Health);
        }

        [TestMethod]
        public void IncreaseHealth_ZeroValue_HealthRemainsSame()
        {
            // Arrange
            double initialHealth = animal.Health;
            double increaseAmount = 0.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth, animal.Health);
        }

        [TestMethod]
        public void IncreaseHealth_NegativeValue_HealthRemainsSame()
        {
            // Arrange
            double initialHealth = animal.Health;
            double increaseAmount = -5.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth, animal.Health);
        }
    }
}
