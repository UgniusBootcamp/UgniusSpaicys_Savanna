using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.MovementStrategies;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class RemoveDeadAnimals
    {
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            IMovement random = new RandomMovement();
            map = new Map(20, 20);
            var animal1 = new Lion(1, 0, 0, "L", 1, 1, 1, random);
            var animal2 = new Lion(1, 1, 1, "L", 1, 1, 1, random);
            var animal3 = new Lion(1, 1, 2, "L", 1, 1, 1, random);
            var animal4 = new Lion(1, 1, 3, "L", 1, 1, 1, random);
            map.SetAnimal(animal1);
            map.SetAnimal(animal2);
            map.SetAnimal(animal3);
            map.SetAnimal(animal4);
        }

        [TestMethod]
        public void RemoveDeadAnimals_ShouldRemoveAllDeadAnimals()
        {
            // Arrange
            var animal1 = map.GetAnimal(0, 0);
            var animal2 = map.GetAnimal(1, 1);
            var animal3 = map.GetAnimal(1, 2);
            var animal4 = map.GetAnimal(1, 3);

            var expectedCount = 2;

            // Act
            animal1.Death();
            animal3.Death();

            map.RemoveDeadAnimals();

            // Assert
            Assert.IsNull(map.GetAnimal(0, 0));
            Assert.IsNotNull(map.GetAnimal(1, 1));
            Assert.IsNull(map.GetAnimal(1, 2));
            Assert.IsNotNull(map.GetAnimal(1, 3));
            Assert.AreEqual(map.Animals.Count, expectedCount);
        }

        [TestMethod]
        public void RemoveDeadAnimals_ShouldNotRemoveAliveAnimals()
        {
            // Arrange
            var animal1 = map.GetAnimal(0, 0);
            var animal2 = map.GetAnimal(1, 1);
            var animal3 = map.GetAnimal(1, 2);
            var animal4 = map.GetAnimal(1, 3);

            // Act
            animal1.Death();
            animal3.Death();

            map.RemoveDeadAnimals();

            // Assert
            Assert.IsTrue(animal2.IsAlive);
            Assert.IsTrue(animal4.IsAlive);
        }
    }
}
