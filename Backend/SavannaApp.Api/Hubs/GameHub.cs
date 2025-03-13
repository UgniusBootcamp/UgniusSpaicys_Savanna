using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities;

namespace SavannaApp.Api.Hubs
{
    public class GameHub(IMapper mapper, IGamesManager gamesManager, IGameCreationService gameCreationService) : Hub
    {
        private static readonly Dictionary<string, string> _userConnections = new Dictionary<string, string>();

        public async Task SendGameData(Game game)
        {

            if (_userConnections.TryGetValue(game.UserId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveGameData", mapper.Map<GameReadDto>(game));
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveGameData", "User not connected");
            }
        }

        public Task CreateGame(GameCreateDto gameCreateDto)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && _userConnections.ContainsKey(userId))
            {
                var game = gameCreationService.CreateGame(userId, gameCreateDto);
                gamesManager.AddGame(game);
            }

            return Task.CompletedTask;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
            {
                _userConnections[userId] = Context.ConnectionId;
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && _userConnections.ContainsKey(userId))
            {
                _userConnections.Remove(userId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
