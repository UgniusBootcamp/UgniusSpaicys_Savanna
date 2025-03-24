using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services.Web
{
    public class WebGameRunner
    {
        public Game Game;
        private bool _isRunning = false;
        object _lock = new object();
        private readonly IGameUpdateInformer _gameUpdateInformer;
        private readonly IAnimalCreationService _animalCreationService;
        private readonly IAnimalGroupManager _animalGroupManager;

        public WebGameRunner(Game game, HunterMovement hm, PrayMovement pm, IAnimalConfigurationService animalConfigurationService, IGameUpdateInformer gameUpdateInformer, IMapManager mapManager)
        {
            Game = game;
            _gameUpdateInformer = gameUpdateInformer;
            var animalFactory = new AnimalFactory(hm, pm, animalConfigurationService);
            _animalCreationService = new AnimalCreationService(animalFactory, mapManager);
            _animalGroupManager = new AnimalGroupManager(_animalCreationService);

            game.IsRunning = true;
            _isRunning = true;
            Task.Run(() => RunGame(Game));
        }

        /// <summary>
        /// Method to run new game
        /// </summary>
        /// <param name="game">game</param>
        private void RunGame(Game game)
        {

            while (_isRunning)
            {
                lock (_lock)
                {
                    if (!game.IsRunning)
                    {
                        Monitor.Wait(_lock);
                    }
                }

                game.Iteration++;

                _animalGroupManager.Reproduction(game.Map);

                var animals = game.Map.Animals.ToList();

                foreach (var prays in animals.Where(a => a is Pray))
                {
                    prays.Move(game.Map);
                }


                foreach (var hunter in animals.Where(a => a is Hunter))
                {
                    hunter.Move(game.Map);
                }

                lock (_lock)
                {
                    game.Map.RemoveDeadAnimals();
                }

                _gameUpdateInformer.NotifyGameUpdated(game);

                Task.Delay(1000).Wait();
            }
        }

        /// <summary>
        /// Method to add animal
        /// </summary>
        /// <param name="animalType">animal type to add</param>
        public void AddAnimal(Type animalType)
        {
            var animal = _animalCreationService.CreateAnimal(animalType, Game.Map);

            if (animal != null) Game.Map.SetAnimal(animal);
        }

        /// <summary>
        /// Method to pause game
        /// </summary>
        public void PauseGame() 
        {
            Game.IsRunning = false;
            _gameUpdateInformer.NotifyGameUpdated(Game);
        }

        /// <summary>
        /// Method to stop game
        /// </summary>
        public void StopGame()
        {
            Game.IsRunning = true;
            _isRunning = false;
        }

        /// <summary>
        /// Method to resume game
        /// </summary>
        public void ResumeGame() 
        {
            lock (_lock)
            {
                Game.IsRunning = true;
                _gameUpdateInformer.NotifyGameUpdated(Game);
                Monitor.Pulse(_lock);
            }
        }
    }
}
