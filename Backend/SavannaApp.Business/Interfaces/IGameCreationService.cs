using Microsoft.Extensions.Configuration.UserSecrets;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGameCreationService
    {
        /// <summary>
        /// Method to create game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameCreateDto">game creation dto</param>
        /// <returns>created game</returns>
        Game CreateGame(string userId, GameCreateDto gameCreateDto);
    }
}
