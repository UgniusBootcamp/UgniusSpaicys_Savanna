using SavannaApp.Animals;
using SavannaApp.Business.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.AssemblyLoaderTests
{
    [TestClass]
    public class LoadAnimalTypes
    {
        private IAssemblyLoader assemblyLoader = null!;
        [TestInitialize]
        public void Setup()
        {
            assemblyLoader = AssemblyLoaderMock.AssemblyLoader;
        }

        [TestMethod]
        public void LoadAnimalTypes_LoadAllAnimalsTypesFromPLuginsDir_ShouldLoadAll()
        {
            //Arrange
            var expected = new List<Type> { typeof(Zebra), typeof(Hyena), typeof(Antelope), typeof(Lion), typeof(Tiger) };

            //Act
            var result = assemblyLoader.LoadAnimalTypes("");

            //Assert
            foreach (var type in expected) 
            {
                Assert.IsTrue(result.Contains(type));
            }
        }

        [TestMethod]
        public void LoadAnimalTypes_DirDoesNotExist_ShouldReturnEmptyList() 
        {
            var randomDir = "DirectoryThatDoesNotExist";

            //Act
            var result = assemblyLoader.LoadAnimalTypes(randomDir);

            //Assert
            Assert.IsTrue(!result.Any());
        }
    }
}
