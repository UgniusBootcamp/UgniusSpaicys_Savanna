using SavannaApp.Data.Entities;
using SavannaApp.Data.Util;
using SavannaApp.Data.Entities.MovementStrategies;
using SavannaApp.Data.Factory;
using SavannaApp.Data.Interfaces.Game;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Tests.Util.AnimalCreationTests
{
    [TestClass]
    public class CreateAnimal
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
        public void CreateAnimal_NoSpaceLeft_ShouldReturnNull()
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
        public void CreateAnimal_CreateAnimal_ShouldReturnAntelope()
        {
            //Arrange
            IMap map = new Map(3, 3);

            //Act
            var result = _animalCreationService.CreateAnimal(typeof(Antelope), map);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Antelope));
        }
    }
}
