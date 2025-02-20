using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    
    [TestClass]
    public class Death
    {
        private Animal animal = null!;

        [TestInitialize]
        public void Setup()
        {
            animal = AnimalMock.CreateLion();
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
