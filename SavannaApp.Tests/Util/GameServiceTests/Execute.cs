using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Tests.Helpers;

namespace SavannaApp.Tests.Util.GameServiceTests
{
    [TestClass]
    public class Execute
    {
        private IGameService _gameService = null!;
        private Mock<IMapCreator> _mapCreatorMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _mapCreatorMock = new Mock<IMapCreator>();
            var creator = AnimalCreationMock.AnimalCreationService;
            _gameService = new GameService(CreateableMapperMock.CreatableMapper, AssemblyLoaderMock.AssemblyLoader, _mapCreatorMock.Object, UIMock.MapPrinterMock.Object, creator, AnimalGroupManagerMock.CreateAnimalGroupManager(creator));
        }

        [TestMethod]
        public void Execute_ShouldRunInEndlessLoop()
        {
            // Arrange
            var mapMock = new Mock<IMap>();

            mapMock.Setup(map => map.Animals).Returns(new List<Animal>
            {
                AnimalMock.CreateLion()
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
