using SavannaApp.Animals;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.CreatableMapperTests
{
    [TestClass]
    public class MapCreatableAnimals
    {
        private ICreatableMapper creatableMapper = null!;
        private List<Type> animalTypes = new List<Type>();
        private Animal lion = null!;
        private Animal antelope = null!;

        [TestInitialize]
        public void Setup()
        {
            creatableMapper = CreateableMapperMock.CreatableMapper;
            animalTypes = [typeof(Antelope), typeof(Lion)];
            lion = AnimalMock.CreateLion();
            antelope = AnimalMock.CreateAntelope(2);
        }

        [TestMethod]
        public void MapCreatableAnimals_ShouldReturnCorrectMapping()
        {
            // Act
            var result = creatableMapper.MapCreatableAnimals(animalTypes);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(animalTypes.Count(), result.Count);
            Assert.IsTrue(result.ContainsKey(antelope.CreationKey));
            Assert.IsTrue(result.ContainsKey(lion.CreationKey));
            Assert.AreEqual(typeof(Lion), result[lion.CreationKey]);
            Assert.AreEqual(typeof(Antelope), result[antelope.CreationKey]);
        }

        [TestMethod]
        public void MapCreatableAnimals_ShouldHandleEmptyList()
        {
            // Arrange
            var types = new List<Type>();

            // Act
            var result = creatableMapper.MapCreatableAnimals(types);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
