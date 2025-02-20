using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class IncreaseHealth
    {
        private Animal animal = null!;

        [TestInitialize]
        public void Setup()
        {
            animal = AnimalMock.CreateLion();
        }

        [TestMethod]
        public void IncreaseHealth_PositiveValue_IncreasesHealth()
        {
            // Arrange
            double initialHealth = animal.Features.Health;
            double increaseAmount = 5.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth + increaseAmount, animal.Features.Health);
        }

        [TestMethod]
        public void IncreaseHealth_ZeroValue_HealthRemainsSame()
        {
            // Arrange
            double initialHealth = animal.Features.Health;
            double increaseAmount = 0.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth, animal.Features.Health);
        }

        [TestMethod]
        public void IncreaseHealth_NegativeValue_HealthRemainsSame()
        {
            // Arrange
            double initialHealth = animal.Features.Health;
            double increaseAmount = -5.0;

            // Act
            animal.IncreaseHealth(increaseAmount);

            // Assert
            Assert.AreEqual(initialHealth, animal.Features.Health);
        }
    }
}
