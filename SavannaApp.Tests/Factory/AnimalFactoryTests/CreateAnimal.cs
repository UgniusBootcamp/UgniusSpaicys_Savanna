using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Factory;
using SavannaApp.Data.Entities.MovementStrategies;
using Moq;
using SavannaApp.Data.Interfaces.Map;

namespace SavannaApp.Tests.Factory.AnimalFactoryTests
{
    [TestClass]
    public class CreateAnimal
    {
        private IAnimalFactory _factory = null!;

        [TestInitialize]
        public void Setup()
        {
            var mapManager = new Mock<IMapManager>();
            var hunterMovement = new HunterMovement(mapManager.Object);
            var prayMovement = new PrayMovement(mapManager.Object);

            _factory = new AnimalFactory(hunterMovement, prayMovement);
        }
        [TestMethod]
        public void CreateAnimal_ShouldReturnLion_WhenAnimalTypeIsLion()
        {
            // Arrange
            var animalType = typeof(Lion);
            var x = 5;
            var y = 10;

            // Act
            var animal = _factory.CreateAnimal(animalType, x, y);

            // Assert
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Lion));
            Assert.AreEqual(x, animal.Position.X);
            Assert.AreEqual(y, animal.Position.Y);
        }

        [TestMethod]
        public void CreateAnimal_ShouldReturnAntelope_WhenAnimalTypeIsAntelope()
        {
            // Arrange
            var animalType = typeof(Antelope);
            var x = 15;
            var y = 20;

            // Act
            var animal = _factory.CreateAnimal(animalType, x, y);

            // Assert
            Assert.IsNotNull(animal);
            Assert.IsInstanceOfType(animal, typeof(Antelope));
            Assert.AreEqual(x, animal.Position.X);
            Assert.AreEqual(y, animal.Position.Y);
        }
    }
}
