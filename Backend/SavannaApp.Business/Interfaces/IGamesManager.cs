using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGamesManager
    {
        /// <summary>
        /// Method to add game
        /// </summary>
        /// <param name="game">game to add</param>
        void AddGame(Game game);

        /// <summary>
        /// Method to remove game
        /// </summary>
        /// <param name="userId"></param>
        void RemoveGame(string userId);

        /// <summary>
        /// Method to add animal to game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="animalType">animal type</param>
        void AddAnimal(string userId, Type animalType);

        /// <summary>
        /// Method to check if game exists
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>true if exists, false if not</returns>
        bool GameExist(string userId);

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
        /// Method to get game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>game</returns>
        Game? GetGame(string userId);
    }
}
