using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class SetAnimal
    {
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
        }

        [TestMethod]
        public void SetAnimal_ValidPosition_AnimalIsSet()
        {
            // Arrange
            var animal = AnimalMock.CreateLion(1, 5, 5);

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
            var animal = AnimalMock.CreateLion(1,25,25);

            // Act and Assert
            map.SetAnimal(animal);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetAnimal_AnimalAlreadyExists_AnimalIsUpdated()
        {
            // Arrange
            var animal1 = AnimalMock.CreateLion(1, 5, 5);
            var animal2 = AnimalMock.CreateLion(2, 5, 5);


            // Act and Assert
            map.SetAnimal(animal1);
            map.SetAnimal(animal2);
        }

        [TestMethod]
        public void SetAnimal_ManyAnimalsAreSet_AnimalsShouldBeSet()
        {
            // Arrange
            var animal1 = AnimalMock.CreateLion(1, 5, 5);
            var animal2 = AnimalMock.CreateLion(2, 5, 6);
            var animal3 = AnimalMock.CreateLion(3, 5, 7);
            var animal4 = AnimalMock.CreateLion(4, 5, 8);


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
