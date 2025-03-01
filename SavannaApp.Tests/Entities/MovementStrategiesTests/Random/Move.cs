﻿using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Entities.MovementStrategiesTests.Random
{
    [TestClass]
    public class Move
    {
        private Animal lion = null!;
        private IMovement random = null!;
        private IMap map = null!;

        [TestInitialize]
        public void Setup()
        {
            random = MovementMock.Random;
            map = new Map(4, 4);
            lion = AnimalMock.CreateLion(1,1,1);
        }

        [TestMethod]
        public void Move_ShouldNotExceedBuffer() 
        {
            //Arrange
            var xStart = 0;
            var xEnd = map.Width;
            var yStart = 0;
            var yEnd = map.Height;
            map.SetAnimal(lion);

            //Act and Assert
            for (int i = 0; i < 100; i++) 
            {
                random.Move(lion, map);
                Assert.IsTrue(xStart != lion.Position.X);
                Assert.IsTrue(xEnd != lion.Position.X);
                Assert.IsTrue(yStart != lion.Position.Y);
                Assert.IsTrue(yEnd != lion.Position.Y);
            }
        }
    }
}
