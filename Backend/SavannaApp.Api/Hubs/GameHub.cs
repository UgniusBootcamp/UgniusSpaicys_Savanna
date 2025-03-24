using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Business.Services;
using SavannaApp.Data.Dto.Game;

namespace SavannaApp.Api.Hubs
{
    public class GameHub(IGameHubService gameHubService) : Hub
    {
        /// <summary>
        /// Endpoint for game creation
        /// </summary>
        /// <param name="gameCreateDto"></param>
        [Authorize]
        public Task CreateGame(GameCreateDto gameCreateDto)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && ConnectionMapper.ConnectionExists(userId))
                gameHubService.CreateGame(userId, gameCreateDto);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Endpoint to load game
        /// </summary>
        /// <param name="gameId">gameId to load</param>
        [Authorize]
        public async Task LoadGame(string gameId)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && ConnectionMapper.ConnectionExists(userId))
                await gameHubService.LoadGame(userId, gameId);
        }

        /// <summary>
        /// Endpoint to save game
        /// </summary>
        [Authorize]
        public async Task SaveGame()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
                await gameHubService.SaveGame(userId);
        }

        /// <summary>
        /// Endpoint to stop game
        /// </summary>
        [Authorize]
        public Task StopGame()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (!string.IsNullOrEmpty(userId))
                gameHubService.RemoveGame(userId);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Endpoint to pause game
        /// </summary>
        [Authorize]
        public Task PauseGame()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
                gameHubService.PauseGame(userId);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Endpoint to resume game
        /// </summary>
        [Authorize]
        public Task ResumeGame() 
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
                gameHubService.ResumeGame(userId);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Endpoint to create animali
        /// </summary>
        /// <param name="animalTypeId">animal type id</param>
        [Authorize]
        public Task CreateAnimal(int animalTypeId)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
                gameHubService.CreateAnimal(userId, animalTypeId);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Endpoint to connect to hub
        /// </summary>
        [Authorize]
        public override Task OnConnectedAsync()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
                ConnectionMapper.Add(userId, Context!.ConnectionId);

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Endpoint to disconnect from hub
        /// </summary>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectionMapper.Remove(userId);
                gameHubService.RemoveGame(userId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
