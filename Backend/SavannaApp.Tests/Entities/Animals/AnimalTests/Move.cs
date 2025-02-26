using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private Animal antelope = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            lion = AnimalMock.CreateLion(1, 9, 9,MovementMock.Hunter);
            antelope = AnimalMock.CreateAntelope(2, 10, 10, MovementMock.Pray);

            map.SetAnimal(antelope);
            map.SetAnimal(lion);
        }

        [TestMethod]
        public void Lion_Move_ShouldEatAntelope()
        {
            // Arrange
            var lionX = lion.Position.X;
            var lionY = lion.Position.Y;

            var antelopeX = antelope.Position.X;
            var antelopeY = antelope.Position.Y;

            var lionHealth = lion.Features.Health;

            // Act
            lion.Move(map);

            // Assert
            Assert.AreEqual(antelopeX, lion.Position.X);
            Assert.AreEqual(antelopeY, lion.Position.Y);

            Assert.AreNotEqual(lionX, lion.Position.X);
            Assert.AreNotEqual(lionY, lion.Position.Y);

            Assert.IsFalse(antelope.IsAlive);
            Assert.IsTrue(lion.IsAlive);
            Assert.IsTrue(lionHealth < lion.Features.Health);
        }

        [TestMethod]
        public void Antelope_Move_ShouldRunFromLion()
        {
            // Arrange
            var lionX = lion.Position.X;
            var lionY = lion.Position.Y;

            var antelopeX = antelope.Position.X;
            var antelopeY = antelope.Position.Y;

            var antelopeHealth = antelope.Features.Health;

            // Act
            antelope.Move(map);

            // Assert
            Assert.IsTrue(antelopeX != antelope.Position.X || antelopeY != antelope.Position.Y);
            Assert.IsTrue(lionX != antelope.Position.X && lionY != antelope.Position.Y);
            Assert.IsTrue(antelope.IsAlive);
            Assert.IsTrue(antelope.Features.Health < antelopeHealth);
        }

        [TestCleanup]
        public void Cleanup()
        {
            map.Animals.RemoveAll(a => true);
        }
    }
}
