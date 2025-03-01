﻿using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.MapCreationTests
{
    [TestClass]
    public class CreateMap
    {
        private IMapCreator mapCreator = null!;
        private Mock<IInputHandler> inputHandler = null!;

        [TestInitialize]
        public void Setup()
        {
            inputHandler = new Mock<IInputHandler>();
            mapCreator = MapCreatorMock.CreateMapCreator(UIMock.OutputHandlerMock.Object, inputHandler.Object);
        }
        [TestMethod]
        public void CreateMap_ShouldReturnValidMap()
        {
            // Arrange
            var expectedHeight = 10;
            var expectedWidth = 10;
            inputHandler.Setup(i => i.GetInt(It.IsAny<string>())).Returns(expectedHeight);
            inputHandler.Setup(i => i.GetInt(It.IsAny<string>())).Returns(expectedWidth);

            // Act
            var map = mapCreator.CreateMap();

            // Assert
            Assert.IsNotNull(map);
            Assert.AreEqual(expectedHeight, map.Height);
            Assert.AreEqual(expectedWidth, map.Width);
        }

        [TestMethod]
        public void CreateMap_ShouldReturnValidMapWithDefaultValues()
        {
            // Arrange
            var expectedHeight = -20;
            var expectedWidth = -20;
            inputHandler.Setup(i => i.GetInt(It.IsAny<string>())).Returns(expectedHeight);
            inputHandler.Setup(i => i.GetInt(It.IsAny<string>())).Returns(expectedWidth);

            // Act
            var map = mapCreator.CreateMap();

            // Assert
            Assert.IsNotNull(map);
            Assert.AreNotEqual(expectedHeight, map.Height);
            Assert.AreNotEqual(expectedWidth, map.Width);
        }
    }
}
