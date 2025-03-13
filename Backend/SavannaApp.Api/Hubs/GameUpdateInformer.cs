using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Api.Hubs
{
    public class GameUpdateInformer(IHubContext<GameHub> context, IMapper mapper) : IGameUpdateInformer
    {
        public async Task NotifyGameUpdated(Game game)
        {
            var connectionId = ConnectionMapper.GetConnectionId(game.UserId);

            if (!string.IsNullOrEmpty(connectionId))
            {
                var gameDto = mapper.Map<GameReadDto>(game);

                await context.Clients.Client(connectionId)
                    .SendAsync("ReceiveGameData", gameDto);
            }
        }
    }
}
