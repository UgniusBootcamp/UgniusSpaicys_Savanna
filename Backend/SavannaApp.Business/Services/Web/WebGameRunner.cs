﻿using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Helpers.MovementStrategies;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services.Web
{
    public class WebGameRunner
    {
        public Game Game;
        public bool _isRunning = false;
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

            _isRunning = true;
            Task.Run(() => RunGame(Game));
        }

        private void RunGame(Game game)
        {
            object _lock = new object();

            while (_isRunning)
            {
                game.Iteration++;

                _animalGroupManager.Reproduction(game.Map);

                var animals = game.Map.Animals.ToList();

                foreach (var animal in animals)
                {
                    animal.Move(game.Map);
                }

                lock (_lock)
                {
                    game.Map.RemoveDeadAnimals();
                }

                _gameUpdateInformer.NotifyGameUpdated(game);

                Task.Delay(1000).Wait();
            }
        }

        public void AddAnimal(Type animalType)
        {
            var animal = _animalCreationService.CreateAnimal(animalType, Game.Map);

            if (animal != null) Game.Map.SetAnimal(animal);
        }

        public void StopGame() => _isRunning = false;
    }
}
