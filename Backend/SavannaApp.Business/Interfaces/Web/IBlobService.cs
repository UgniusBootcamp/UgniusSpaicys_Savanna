using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IBlobService
    {
        public Task SaveGameAsync(Game game);
        public Task<Game?> LoadGameAsync(string userId, string gameId);
        public Task<IEnumerable<GameLoadInfoDto>> GetUserGamesAsync(string userId, DateOnly start, DateOnly end);
    }
}
