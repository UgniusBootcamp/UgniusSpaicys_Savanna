using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class RemoveDeadAnimals
    {
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            var animal1 = AnimalMock.CreateLion();
            var animal2 = AnimalMock.CreateLion(2,1,1);
            var animal3 = AnimalMock.CreateLion(3,1,2);
            var animal4 = AnimalMock.CreateLion(4,1,3);
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
