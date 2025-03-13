using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGamesManager
    {
        void AddGame(Game game);
        void RemoveGame(string userId);
    }
}
