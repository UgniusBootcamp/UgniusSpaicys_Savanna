using Moq;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.UI;

namespace SavannaApp.Tests.Helpers
{
    public static class UIMock
    {
        public static Mock<IInputHandler> InputHandlerMock = new Mock<IInputHandler>();
        public static Mock<IOutputHandler> OutputHandlerMock = new Mock<IOutputHandler>();
        public static Mock<IMapPrinter> MapPrinterMock = new Mock<IMapPrinter>();
    }
}
