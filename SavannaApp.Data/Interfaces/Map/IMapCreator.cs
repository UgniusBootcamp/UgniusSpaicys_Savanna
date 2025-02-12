using SavannaApp.Data.Interfaces.Game;

namespace GameOfLife.Data.Interfaces.Game
{
    public interface IMapCreator
    {
        IMap CreateMap();
    }
}