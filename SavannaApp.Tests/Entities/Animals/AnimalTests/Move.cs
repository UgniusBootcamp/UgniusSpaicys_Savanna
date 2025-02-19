using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Entities.Animals.AnimalTests
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private Animal antelope = null!;
        private IMovement hunter = null!;
        private IMovement pray = null!;
        private IMapManager mapManager = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            mapManager = new MapManager();
            hunter = new HunterMovement(mapManager);
            pray = new PrayMovement(mapManager);
            lion = new Lion(1, 9, 9, "L", 5, 5, 5, hunter);
            antelope = new Antelope(2, 10, 12, "A", 5, 5, 5, pray);

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

            var lionHealth = lion.Health;

            // Act
            lion.Move(map);

            // Assert
            Assert.AreEqual(antelopeX, lion.Position.X);
            Assert.AreEqual(antelopeY, lion.Position.Y);

            Assert.AreNotEqual(lionX, lion.Position.X);
            Assert.AreNotEqual(lionY, lion.Position.Y);

            Assert.IsFalse(antelope.IsAlive);
            Assert.IsTrue(lion.IsAlive);
            Assert.IsTrue(lionHealth < lion.Health);
        }

        [TestMethod]
        public void Antelope_Move_ShouldRunFromLion()
        {
            // Arrange
            var lionX = lion.Position.X;
            var lionY = lion.Position.Y;

            var antelopeX = antelope.Position.X;
            var antelopeY = antelope.Position.Y;

            var antelopeHealth = antelope.Health;

            // Act
            antelope.Move(map);

            // Assert
            Assert.IsTrue(antelopeX != antelope.Position.X || antelopeY != antelope.Position.Y);
            Assert.IsTrue(lionX != antelope.Position.X && lionY != antelope.Position.Y);
            Assert.IsTrue(antelope.IsAlive);
            Assert.IsTrue(antelope.Health < antelopeHealth);
        }

        [TestCleanup]
        public void Cleanup()
        {
            map.Animals.RemoveAll(a => true);
        }
    }
}
