using Moq;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class MovementMock
    {
        public static Mock<IMovement> Movement = new Mock<IMovement>();

        public static HunterMovement Hunter = new HunterMovement(MapManagerMock.mapManager);

        public static PrayMovement Pray = new PrayMovement(MapManagerMock.mapManager);

        public static RandomMovement Random = new RandomMovement();
    }
}
