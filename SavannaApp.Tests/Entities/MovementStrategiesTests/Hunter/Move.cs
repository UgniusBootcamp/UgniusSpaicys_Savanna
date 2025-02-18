﻿using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Interfaces.Map;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Util;

namespace SavannaApp.Tests.Entities.MovementStrategiesTests.Hunter
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
            var lionHealth = lion.Health;
            var antelopeX = antelope.Position.X;
            var antelopeY = antelope.Position.Y;

            //Act
            var result = hunter.Move(lion, map);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsFalse(antelope.IsAlive);
            Assert.IsTrue(lion.Health > lionHealth);
            Assert.AreEqual(antelope.Health, 0);
            Assert.IsTrue(lion.Position.X == antelopeX && lion.Position.Y == antelopeY);
        }

        [TestMethod]
        public void Move_InVisionButNotEnoughSpeed_ShouldReturnTrue()
        {
            //Arrange
            var closeLion = new Lion(1, 9, 9, "L", 2, 5, 5, hunter);
            map.SetAnimal(closeLion);
            map.SetAnimal(antelope);
            var expected = true;
            var lionHealth = closeLion.Health;
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
