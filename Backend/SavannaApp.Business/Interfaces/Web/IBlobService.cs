using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IBlobService
    {
        /// <summary>
        /// Method to save game 
        /// </summary>
        /// <param name="game">game to save</param>
        public Task SaveGameAsync(Game game);

        /// <summary>
        /// Method to load game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameId">game id</param>
        public Task<Game?> LoadGameAsync(string userId, string gameId);

        /// <summary>
        /// Method to get user games
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="start">start range</param>
        /// <param name="end">end range</param>
        /// <returns>user games in range</returns>
        public Task<IEnumerable<GameLoadInfoDto>> GetUserGamesAsync(string userId, DateOnly start, DateOnly end);
    }
}
