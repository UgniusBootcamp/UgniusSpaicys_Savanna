using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Api.Hubs
{
    public class GameUpdateInformer(IHubContext<GameHub> context, IMapper mapper) : IGameUpdateInformer
    {
        /// <summary>
        /// Method to notify user with game update
        /// </summary>
        /// <param name="game">game</param>
        /// <returns>user informed with updated game</returns>
        public async Task NotifyGameUpdated(Game game)
        {
            var connectionId = ConnectionMapper.GetConnectionId(game.UserId);

            if (!string.IsNullOrEmpty(connectionId))
            {
                var gameDto = mapper.Map<GameReadDto>(game);

                await context.Clients.Client(connectionId)
                    .SendAsync(EndpointConstants.ReceiveGameData, gameDto);
            }
        }
    }
}
