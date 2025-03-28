﻿using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MovementStrategiesTests.Pray
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private Animal antelope = null!;
        private IMovement pray = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            map = new Map(20, 20);
            lion = AnimalMock.CreateLion(1, 9, 9, MovementMock.Movement.Object);

            pray = MovementMock.Pray;
            antelope = AnimalMock.CreateAntelope(2, 10, 10, pray);
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
            antelope = AnimalMock.CreateAntelope(2, 1, 1, pray);

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
