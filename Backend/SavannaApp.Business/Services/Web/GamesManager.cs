using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services.Web
{
    public class GamesManager(
        HunterMovement hunterMovement,
        PrayMovement prayMovement,
        IAnimalConfigurationService animalConfigurationService,
        IGameUpdateInformer gameUpdateInformer,
        IMapManager mapManager)
        : IGamesManager
    {
        private static List<WebGameRunner> WebGames = new List<WebGameRunner>();

        /// <summary>
        /// Method to add game
        /// </summary>
        /// <param name="game">game</param>
        public void AddGame(Game game)
        {
            if (WebGames.All(g => g.Game.UserId != game.UserId))
            {
                var webGame = new WebGameRunner(
                    game,
                    hunterMovement,
                    prayMovement,
                    animalConfigurationService,
                    gameUpdateInformer,
                    mapManager);

                WebGames.Add(webGame);
            }
        }

        /// <summary>
        /// Method to remove game
        /// </summary>
        /// <param name="userId">user id</param>
        public void RemoveGame(string userId)
        {
            var gameToRemove = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (gameToRemove != null)
            {
                gameToRemove.StopGame();
                WebGames.Remove(gameToRemove);
            }
        }

        /// <summary>
        /// Method to add animal
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="animalType">animal type</param>
        public void AddAnimal(string userId, Type animalType)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (game != null) 
            {
                game.AddAnimal(animalType);
            }
        }

        /// <summary>
        /// Method to check if game exists
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>true if exists, false if not</returns>
        public bool GameExist(string userId)
        {
            return WebGames.FirstOrDefault(g => g.Game.UserId == userId) != null;
        }

        /// <summary>
        /// Method to pause game
        /// </summary>
        /// <param name="userId">user id</param>
        public void PauseGame(string userId)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if(game != null) game.PauseGame();
        }

        /// <summary>
        /// Method to resume game
        /// </summary>
        /// <param name="userId">user id</param>
        public void ResumeGame(string userId)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (game != null) game.ResumeGame();
        }

        /// <summary>
        /// Method to get game
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>game</returns>
        public Game? GetGame(string userId)
        {
            var gameRunner = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (gameRunner != null) return gameRunner.Game;

            return null;
        }
    }
}
