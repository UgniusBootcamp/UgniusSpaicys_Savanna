using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    
    [TestClass]
    public class Death
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
        public void Death_CallFunction_ShouldReturnFalse()
        {
            //Arrange
            var expected = false;
            var isAlive = animal.IsAlive;

            //Act
            animal.Death();
            var isAliveAfter = animal.IsAlive;

            //Assert
            Assert.AreEqual(isAlive, true);
            Assert.AreEqual(expected, isAliveAfter);
        }

    }
}
