using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Util.GameServiceTests
{
    [TestClass]
    public class Execute
    {
        private IGameService _gameService = null!;
        private Mock<IMapCreator> _mapCreatorMock = null!;
        private Mock<IMapPrinter> _printerMock = null!;
        private Mock<IAnimalCreationService> _animalCreationServiceMock = null!;
        private Mock<IAnimalGroupManager> _animalGroupManager = null!;
        private Mock<IMovement> random = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapCreatorMock = new Mock<IMapCreator>();
            _printerMock = new Mock<IMapPrinter>();
            _animalCreationServiceMock = new Mock<IAnimalCreationService>();
            _animalGroupManager = new Mock<IAnimalGroupManager>();
            random = new Mock<IMovement>();

            _gameService = new GameService(_mapCreatorMock.Object, _printerMock.Object, _animalCreationServiceMock.Object, _animalGroupManager.Object);
        }

        [TestMethod]
        public void Execute_ShouldRunInEndlessLoop()
        {
            // Arrange
            var mapMock = new Mock<IMap>();

            mapMock.Setup(map => map.Animals).Returns(new List<Animal>
            {
                new Lion(1,1,1,"L",1,1,1, random.Object),
            });
            _mapCreatorMock.Setup(m => m.CreateMap()).Returns(mapMock.Object);

            // Act & Assert
            Assert.ThrowsException<TimeoutException>(() =>
            {
                var task = Task.Run(() => _gameService.Execute());
                if (!task.Wait(TimeSpan.FromSeconds(5)))
                {
                    throw new TimeoutException();
                }
            });
        }
    }
}
