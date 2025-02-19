using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using Moq;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.MovementStrategies;

namespace SavannaApp.Tests.Entities.MovementStrategiesTests.Pray
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private Animal antelope = null!;
        private Mock<IMovement> hunter = null!;
        private IMovement pray = null!;
        private IMapManager mapManager = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            mapManager = new MapManager();
            hunter = new Mock<IMovement>();
            pray = new PrayMovement(mapManager);
            lion = new Lion(1, 9, 9, "L", 5, 5, 5, hunter.Object);
            antelope = new Antelope(2, 10, 12, "A", 5, 5, 5, pray);
        }

        [TestMethod]
        public void Move_NoLionsAround_ShouldReturnFalse() 
        {
            //Arrange
            map.SetAnimal(antelope);
            var expected = false;

            //Act
            var result = pray.Move(antelope, map);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Move_AntelopeShouldRunFromLion_ShouldReturnTrue() 
        {
            //Arrange
            map.SetAnimal(antelope);
            map.SetAnimal(lion);
            var expected = true;
            var initialDistance = antelope.DistanceTo(lion.Position.X, lion.Position.Y);

            //Act
            var result = pray.Move(antelope, map);
            var distanceAfterMove = antelope.DistanceTo(lion.Position.X, lion.Position.Y);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsTrue(distanceAfterMove > initialDistance);
        }

        [TestMethod]
        public void Move_NoLionsInVision_ShouldReturnFalse()
        {
            //Arrange
            lion = new Lion(1, 9, 9, "L", 5, 5, 5, hunter.Object);
            antelope = new Antelope(2, 10, 12, "A", 5, 2, 5, pray);

            map.SetAnimal(antelope);
            map.SetAnimal(lion);
            var expected = false;

            //Act
            var result = pray.Move(antelope, map);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCleanup]
        public void Cleanup()
        {
            map = new Map(20, 20);
        }

    }
}
