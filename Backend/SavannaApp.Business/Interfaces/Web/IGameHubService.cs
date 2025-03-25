using SavannaApp.Data.Dto.Game;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IGameHubService
    {
        /// <summary>
        /// Method to create game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameCreateDto">game creation dto</param>
        void CreateGame(string userId, GameCreateDto gameCreateDto);

        /// <summary>
        /// Method to load game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="gameId">game id</param>
        Task LoadGame(string userId, string gameId);

        /// <summary>
        /// Method to save game
        /// </summary>
        /// <param name="userId">user id</param>
        Task SaveGame(string userId);

        /// <summary>
        /// Method to pause game
        /// </summary>
        /// <param name="userId">user id</param>
        void PauseGame(string userId);

        /// <summary>
        /// Method to resume game
        /// </summary>
        /// <param name="userId">user id</param>
        void ResumeGame(string userId);

        /// <summary>
        /// Method to create animal
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="animalTypeId">animal type id</param>
        void CreateAnimal(string userId, int animalTypeId);

        /// <summary>
        /// Method to remove game
        /// </summary>
        /// <param name="userId">user id</param>
        void RemoveGame(string userId);
    }
}
