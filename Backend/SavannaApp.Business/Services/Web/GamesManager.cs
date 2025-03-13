using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Services.Web
{
    public class GamesManager(IAnimalCreationService animalCreationService) : IGamesManager
    {
        private List<Game> Games = new List<Game>();

        public void AddGame(Game game)
        {
            if (Games.All(g => g.UserId != game.UserId))
            {
                Games.Add(game);

                game.Start();

                Task.Run(() => RunGame(game));
            }
        }

        public void RemoveGame(string userId)
        {
            var gameToRemove = Games.FirstOrDefault(g => g.UserId == userId);

            if (gameToRemove != null)
            {
                gameToRemove.Stop();

                Games.Remove(gameToRemove);
            }
        }

        public void AddAnimal(string userId, Animal animal)
        {
            var game = Games.FirstOrDefault(g => g.UserId == userId);

            if (game != null)
                game.Map.SetAnimal(animal);
        }

        private void RunGame(Game game)
        {
            object _lock = new object();
            IAnimalGroupManager animalGroupManager = new AnimalGroupManager(animalCreationService);

            while (game.IsRunning)
            {
                game.Iteration++;

                animalGroupManager.Reproduction(game.Map);

                var animals = game.Map.Animals.ToList();

                foreach (var animal in animals)
                {
                    animal.Move(game.Map);
                }

                lock (_lock)
                {
                    game.Map.RemoveDeadAnimals();
                }

                Task.Delay(1000).Wait();
            }
        }
    }
}
