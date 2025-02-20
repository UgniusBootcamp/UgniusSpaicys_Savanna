using Moq;
using SavannaApp.Data.Helpers.Map;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class MovementMock
    {
        private static readonly IMapManager mapManager = new MapManager();

        public static Mock<IMovement> Movement = new Mock<IMovement>();

        public static HunterMovement Hunter = new HunterMovement(mapManager);

        public static PrayMovement Pray = new PrayMovement(mapManager);
    }
}
