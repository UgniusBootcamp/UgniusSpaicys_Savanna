using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.MovementStrategies;

namespace SavannaApp.Tests.Util.AnimalCreationTests
{
    [TestClass]
    public class RebirthAnimal
    {
        private AnimalCreationService _animalCreationService = null!;

        [TestInitialize]
        public void Setup()
        {
            var mapManager = new MapManager();
            var hunterMovement = new HunterMovement(mapManager);
            var prayMovement = new PrayMovement(mapManager);

            _animalCreationService = new AnimalCreationService(new AnimalFactory(hunterMovement, prayMovement), new MapManager());
        }

        [TestMethod]
        public void RebirthAnimal_NoSpaceLeft_ShouldReturnNull()
        {
            //Arrange
            IMap map = new Map(1, 1);
            map.SetAnimal(new Antelope(1, 0, 0, "A", 1, 1, 10, new RandomMovement()));

            //Act
            var result = _animalCreationService.CreateAnimal(typeof(Antelope), map);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RebirthAnimal_SpaceBetweenAnimals_ShouldReturnMiddlePosition()
        {
            //Arrange
            IMap map = new Map(3, 3);
            map.SetAnimal(new Antelope(1, 0, 0, "A", 1, 1, 10, new RandomMovement()));
            map.SetAnimal(new Antelope(1, 2, 2, "A", 1, 1, 10, new RandomMovement()));

            var expectedX = 1;
            var expectedY = 1;

            //Act
            var result = _animalCreationService.RebirthAnimal(map.Animals, map);

            //Assert
            Assert.AreEqual(result.Position.X, expectedX);
            Assert.AreEqual(result.Position.Y, expectedY);
        }
    }
}
