using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MovementStrategiesTests.Hunter
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private Animal antelope = null!;
        private IMovement hunter = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);

            hunter = MovementMock.Hunter;
            lion = AnimalMock.CreateLion(1, 9, 9, hunter);

            antelope = AnimalMock.CreateAntelope(2, 10, 10, MovementMock.Movement.Object);
        }

        [TestMethod]
        public void Move_NoAntelopesAround_ShouldReturnFalse()
        {
            //Arrange
            map.SetAnimal(lion);
            var expected = false;

            //Act
            var result = hunter.Move(lion, map);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Move_EatAntelope_ShouldReturnTrue()
        {
            //Arrange
            map.SetAnimal(lion);
            map.SetAnimal(antelope);
            var expected = true;
            var lionHealth = lion.Features.Health;
            var antelopeX = antelope.Position.X;
            var antelopeY = antelope.Position.Y;

            //Act
            var result = hunter.Move(lion, map);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsFalse(antelope.IsAlive);
            Assert.IsTrue(lion.Features.Health > lionHealth);
            Assert.AreEqual(antelope.Features.Health, 0);
            Assert.IsTrue(lion.Position.X == antelopeX && lion.Position.Y == antelopeY);
        }

        [TestMethod]
        public void Move_InVisionButNotEnoughSpeed_ShouldReturnTrue()
        {
            //Arrange
            var closeLion = AnimalMock.CreateLion(1, 6, 6, hunter);
            map.SetAnimal(closeLion);
            map.SetAnimal(antelope);
            var expected = true;
            var lionHealth = closeLion.Features.Health;
            var initialDistance = closeLion.DistanceTo(antelope.Position.X, antelope.Position.Y);

            //Act
            var result = hunter.Move(closeLion, map);
            var distanceAfterMove = closeLion.DistanceTo(antelope.Position.X, antelope.Position.Y);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsTrue(antelope.IsAlive);
            Assert.IsTrue(initialDistance > distanceAfterMove);
        }

        [TestCleanup]
        public void Cleanup() 
        {
            map = new Map(20, 20);
        }
    }
}
