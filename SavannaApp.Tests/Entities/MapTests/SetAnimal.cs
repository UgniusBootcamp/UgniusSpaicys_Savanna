using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class SetAnimal
    {
        private IMap map = null!;
        private IMovement random = null!;

        [TestInitialize]
        public void Setup()
        {
            random = new RandomMovement();
            map = new Map(20, 20);
        }

        [TestMethod]
        public void SetAnimal_ValidPosition_AnimalIsSet()
        {
            // Arrange
            var animal = new Lion(1, 5, 5, "Lion", 10, 5, 100, random);

            // Act
            map.SetAnimal(animal);
            var result = map.GetAnimal(5, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(animal, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAnimal_InvalidPosition_AnimalIsNotSet()
        {
            // Arrange
            var animal = new Lion(1, 25, 25, "Lion", 10, 5, 100, random);

            // Act and Assert
            map.SetAnimal(animal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAnimal_AnimalAlreadyExists_AnimalIsUpdated()
        {
            // Arrange
            var animal1 = new Lion(1, 5, 5, "Lion", 10, 5, 100, random);
            var animal2 = new Lion(2, 5, 5, "Lion2", 12, 6, 90, random);

            // Act and Assert
            map.SetAnimal(animal1);
            map.SetAnimal(animal2);
        }

        [TestMethod]
        public void SetAnimal_ManyAnimalsAreSet_AnimalsShouldBeSet()
        {
            // Arrange
            var animal1 = new Lion(1, 5, 5, "Lion", 10, 5, 100, random);
            var animal2 = new Lion(2, 5, 6, "Lion2", 12, 6, 90, random);
            var animal3 = new Lion(3, 5, 7, "Lion2", 12, 6, 90, random);
            var animal4 = new Lion(4, 5, 8, "Lion2", 12, 6, 90, random);

            var expected = 4;

            // Act
            map.SetAnimal(animal1);
            map.SetAnimal(animal2);
            map.SetAnimal(animal3);
            map.SetAnimal(animal4);

            //Assert
            Assert.AreEqual(map.Animals.Count(), expected);
        }
    }
}
