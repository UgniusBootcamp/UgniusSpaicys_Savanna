using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalGroupTests
{
    [TestClass]
    public class GroupStillExist
    {
        private AnimalGroup group = null!;

        [TestInitialize]
        public void Setup()
        {
            var animal = AnimalMock.CreateLion();
            var animal1 = AnimalMock.CreateLion(2, 1);
            group = new AnimalGroup(animal, animal1);
        }

        [TestMethod]
        public void GroupStillExist_ShouldReturnTrue_WhenAnimalsAreAlive()
        {
            // Arrange
            int range = 5;

            // Act
            bool result = group.GroupStillExist(range);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GroupStillExist_ShouldReturnFalse_WhenOneAnimalIsDead()
        {
            // Arrange
            int range = 5;
            group.Animals[0].Death();

            // Act
            bool result = group.GroupStillExist(range);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GroupStillExist_ShouldReturnFalse_WhenBothAnimalsAreDead()
        {
            // Arrange
            int range = 5;
            group.Animals[0].Death();
            group.Animals[1].Death();

            // Act
            bool result = group.GroupStillExist(range);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
