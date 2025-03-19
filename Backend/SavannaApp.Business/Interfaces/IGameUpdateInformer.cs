using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGameUpdateInformer
    {
        Task NotifyGameUpdated(Game game);
    }
}
