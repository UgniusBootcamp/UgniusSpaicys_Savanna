using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;

namespace SavannaApp.Tests.Helpers
{
    public static class AssemblyLoaderMock
    {
        public static IAssemblyLoader AssemblyLoader = new AssemblyLoader();
    }
}
