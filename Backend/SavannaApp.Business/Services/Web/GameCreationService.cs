using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Services.Web
{
    public class GameCreationService : IGameCreationService
    {
        public Game CreateGame(string userId, GameCreateDto gameCreateDto)
        {
            return new Game
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Map = new Map(gameCreateDto.Height, gameCreateDto.Width)
            };
        }

    }
}
