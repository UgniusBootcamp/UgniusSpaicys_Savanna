using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Services;
using SavannaApp.Data.Dto.Game;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Api.Hubs
{
    public class GameHub(IGamesManager gamesManager, IGameCreationService gameCreationService, AnimalTypeMapper animalTypeMapper) : Hub
    {
        public Task CreateGame(GameCreateDto gameCreateDto)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && ConnectionMapper.ConnectionExists(userId))
            {
                var game = gameCreationService.CreateGame(userId, gameCreateDto);
                gamesManager.AddGame(game);
            }

            return Task.CompletedTask;
        }

        public Task PauseGame()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && gamesManager.GameExist(userId)) gamesManager.PauseGame(userId);

            return Task.CompletedTask;
        }

        public Task ResumeGame() 
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && gamesManager.GameExist(userId)) gamesManager.ResumeGame(userId);

            return Task.CompletedTask;
        }

        public Task CreateAnimal(int animalTypeId)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && ConnectionMapper.ConnectionExists(userId))
            {
                var animalType = animalTypeMapper.GetType(animalTypeId);
                if (animalType == null) return Task.CompletedTask;

                gamesManager.AddAnimal(userId, animalType);
            }

            return Task.CompletedTask;
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectionMapper.Add(userId, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (!string.IsNullOrEmpty(userId) && ConnectionMapper.ConnectionExists(userId))
            {
                ConnectionMapper.Remove(userId);
                gamesManager.RemoveGame(userId);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
