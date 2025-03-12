using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Tests.Helpers
{
    public static class MapMock
    {
        public static IMap CreateMap(int height, int width)
        {
            return new Map(height, width);
        }
    }
}
