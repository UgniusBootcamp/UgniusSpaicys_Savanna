using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalGroupTests
{
    [TestClass]
    public class RoundTogether
    {
        private AnimalGroup group = null!;

        [TestInitialize]
        public void Setup()
        {
            var animal = AnimalMock.CreateLion();
            var animal1 = AnimalMock.CreateLion(2);

            group = new AnimalGroup(animal, animal1);
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
