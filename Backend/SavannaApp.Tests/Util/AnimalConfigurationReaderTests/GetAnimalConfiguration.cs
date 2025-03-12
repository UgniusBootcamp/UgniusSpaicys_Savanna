using SavannaApp.Business.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.AnimalConfigurationReaderTests
{
    [TestClass]
    public class GetAnimalConfiguration
    {
        private IAnimalConfigReader _configReader = null!;

        [TestInitialize]
        public void Setup()
        {
            _configReader = AnimalConfigReaderMock.AnimalConfigReader;
        }

        [TestMethod]
        public void GetAnimalConfiguration_FileExist_ReturnsAnimalConfiguration()
        {
            //Arrange
            var configFile = "config.json";

            //Act 
            var result = _configReader.GetAnimalConfiguration(configFile);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetAnimalConfiguration_FileDoesNotExist_ThrowsException()
        {
            //Arrange
            var configFile = "fileWhichDoesNotExist.json";

            //Act and Assert
            var result = _configReader.GetAnimalConfiguration(configFile);
        }
    }
}
