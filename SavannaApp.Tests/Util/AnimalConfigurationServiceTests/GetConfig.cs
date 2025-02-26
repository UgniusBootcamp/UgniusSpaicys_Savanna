using SavannaApp.Business.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.AnimalConfigurationServiceTests
{
    [TestClass]
    public class GetConfig
    {
        private IAnimalConfigurationService _config = null!;

        [TestInitialize]
        public void Setup()
        {
            _config = AnimalConfigurationServiceMock.AnimalConfigurationService;
        }

        [TestMethod]
        public void GetConfig_ConfigExists_ReturnsConfig() 
        {
            var configClasses = new List<string> { "Lion", "Antelope" };
            //Act and Assert

            foreach (var configClass in configClasses) 
            {
                var result = _config.GetConfig(configClass);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void GetConfig_ConfigDoesNotExist_ReturnsNull()
        {
            var configClass = "Hippo";

            //Act
            var result = _config.GetConfig(configClass);
            
            //Assert
            Assert.IsNull(result);
        }
    }
}
