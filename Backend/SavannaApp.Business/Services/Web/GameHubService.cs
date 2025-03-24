using SavannaApp.Business.Interfaces;
using SavannaApp.Business.Interfaces.Web;
using SavannaApp.Data.Dto.Game;

namespace SavannaApp.Business.Services.Web
{
    public class GameHubService(
        IGamesManager gamesManager,
        IGameCreationService gameCreationService,
        AnimalTypeMapper animalTypeMapper,
        IBlobService blobService
        ) : IGameHubService
    {
        public void CreateAnimal(string userId, int animalTypeId)
        {
            if (!gamesManager.GameExist(userId)) return;

            var animalType = animalTypeMapper.GetType(animalTypeId);
            if (animalType == null) return;

            gamesManager.AddAnimal(userId, animalType);
        }

        public void CreateGame(string userId, GameCreateDto gameCreateDto)
        {
            var game = gameCreationService.CreateGame(userId, gameCreateDto);
            gamesManager.AddGame(game);
        }

        public async Task LoadGame(string userId, string gameId)
        {
            var game = await blobService.LoadGameAsync(userId, gameId);
            if (game != null)
                gamesManager.AddGame(game);
        }

        public void PauseGame(string userId)
        {
            gamesManager.PauseGame(userId);
        }

        public void RemoveGame(string userId)
        {
            gamesManager.RemoveGame(userId);
        }

        public void ResumeGame(string userId)
        {
            gamesManager.ResumeGame(userId);
        }

        public async Task SaveGame(string userId)
        {
            var game = gamesManager.GetGame(userId);
            if (game == null || game.IsRunning) return;

            await blobService.SaveGameAsync(game);
        }
    }
}
