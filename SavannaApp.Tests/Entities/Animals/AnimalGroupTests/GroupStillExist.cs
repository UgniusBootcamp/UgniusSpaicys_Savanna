using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Tests.Entities.Animals.AnimalGroupTests
{
    [TestClass]
    public class GroupStillExist
    {
        private AnimalGroup group = null!;

        [TestInitialize]
        public void Setup()
        {
            var random = new RandomMovement();
            var animal = new Lion(1, 2, 2, "L", 1, 1, 1, random);
            var animal1 = new Lion(1, 0, 0, "L", 1, 1, 1, random);

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
