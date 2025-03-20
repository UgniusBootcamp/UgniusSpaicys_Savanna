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

        public void RemoveGame(string userId)
        {
            var gameToRemove = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (gameToRemove != null)
            {
                WebGames.Remove(gameToRemove);
            }
        }

        public void AddAnimal(string userId, Type animalType)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (game != null) 
            {
                game.AddAnimal(animalType);
            }
        }

        public bool GameExist(string userId)
        {
            return WebGames.FirstOrDefault(g => g.Game.UserId == userId) != null;
        }

        public void PauseGame(string userId)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if(game != null) game.StopGame();
        }

        public void ResumeGame(string userId)
        {
            var game = WebGames.FirstOrDefault(g => g.Game.UserId == userId);

            if (game != null) game.ResumeGame();
        }
    }
}
