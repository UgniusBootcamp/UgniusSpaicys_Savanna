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
        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="animalTypeId">animal type id</param>
        public void CreateAnimal(string userId, int animalTypeId)
        {
            if (!gamesManager.GameExist(userId)) return;

            var animalType = animalTypeMapper.GetType(animalTypeId);
            if (animalType == null) return;

            gamesManager.AddAnimal(userId, animalType);
        }

        /// <summary>
        /// Method to create game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameCreateDto">game create dto</param>
        public void CreateGame(string userId, GameCreateDto gameCreateDto)
        {
            var game = gameCreationService.CreateGame(userId, gameCreateDto);
            gamesManager.AddGame(game);
        }

        /// <summary>
        /// Method to load game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameId">game id</param>
        public async Task LoadGame(string userId, string gameId)
        {
            var game = await blobService.LoadGameAsync(userId, gameId);
            if (game != null)
                gamesManager.AddGame(game);
        }

        /// <summary>
        /// Method to pause game
        /// </summary>
        /// <param name="userId">user id</param>
        public void PauseGame(string userId)
        {
            gamesManager.PauseGame(userId);
        }

        /// <summary>
        /// Method to remove game
        /// </summary>
        /// <param name="userId">user id</param>
        public void RemoveGame(string userId)
        {
            gamesManager.RemoveGame(userId);
        }

        /// <summary>
        /// Method to resume game
        /// </summary>
        /// <param name="userId">user id</param>
        public void ResumeGame(string userId)
        {
            gamesManager.ResumeGame(userId);
        }

        /// <summary>
        /// Method to save game
        /// </summary>
        /// <param name="userId">user id</param>
        public async Task SaveGame(string userId)
        {
            var game = gamesManager.GetGame(userId);
            if (game == null || game.IsRunning) return;

            await blobService.SaveGameAsync(game);
        }
    }
}
