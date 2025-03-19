using Microsoft.Extensions.Configuration.UserSecrets;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGameCreationService
    {
        Game CreateGame(string userId, GameCreateDto gameCreateDto);
    }
}
