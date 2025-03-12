using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Tests.Entities.MapTests
{
    [TestClass]
    public class MapConstructor
    {
        [TestMethod]
        public void Constructor_ShouldInitializeWidthAndHeight()
        {
            // Arrange
            int expectedWidth = 10;
            int expectedHeight = 20;

            // Act
            var map = new Map(expectedHeight, expectedWidth);

            // Assert
            Assert.AreEqual(expectedWidth, map.Width);
            Assert.AreEqual(expectedHeight, map.Height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldThrowArgumentException_WhenWidthIsZero()
        {
            // Arrange
            int width = 0;
            int height = 20;

            //Act and Assert
            var map = new Map(width, height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldThrowArgumentException_WhenHeightIsZero()
        {
            // Arrange
            int width = 10;
            int height = 0;

            //Act and Assert
            var map = new Map(width, height);
        }
    }
}
