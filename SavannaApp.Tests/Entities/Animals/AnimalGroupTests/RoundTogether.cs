using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Tests.Entities.Animals.AnimalGroupTests
{
    [TestClass]
    public class RoundTogether
    {
        private AnimalGroup group = null!;

        [TestInitialize]
        public void Setup()
        {
            var random = new RandomMovement();
            var animal = new Lion(1, 2, 2, "L", 1, 1, 1, random);
            var animal1 = new Lion(1, 2, 2, "L", 1, 1, 1, random);

            group = new AnimalGroup(animal1, animal1);
        }

        [TestMethod]
        public void RoundTogether_IncreaseValue_ReturnsCorrectResult() 
        {
            //Arrange
            var expected = 1;
            var baseExpectedValue = 0;

            var baseValue = group.RoundsTogether;
            //Act
            group.RoundTogether();
            var result = group.RoundsTogether;

            //Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(baseExpectedValue, baseValue);
        }
    }
}
