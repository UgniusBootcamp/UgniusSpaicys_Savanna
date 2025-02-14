using GameOfLife.Data.Interfaces.Game;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Util
{
    public class GameService(IMapCreator mapCreator, IMapPrinter mapPrinter, IAnimalCreationService animalCreationService) : IGameService
    {
        private IMap map = null!;
        private bool _isRunning = false;
        private readonly object _lock = new object();

        /// <summary>
        /// Method to run game
        /// </summary>
        public void Execute()
        {
            map = mapCreator.CreateMap();

            _isRunning = true;


            Task.Run(ListenForKeyPress);

            RunLoop();
        }

        /// <summary>
        /// Helper to run a loop of game
        /// </summary>
        private void RunLoop()
        {
            while (_isRunning) 
            {

                var animals = map.Animals.ToList();

                foreach (var antelope in animals.Where(a => a is Antelope))
                {
                    antelope.Move(map);
                }


                foreach (var lion in animals.Where(a => a is Lion))
                {
                    lion.Move(map);
                }

                lock (_lock)
                {
                    map.RemoveDeadAnimals();
                }

                Print();

                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Helper to print map
        /// </summary>
        private void Print()
        {
            var antelopesCount = map.Animals.Where(a => a is Antelope).Count();
            var lionsCount = map.Animals.Where(l => l is Lion).Count();

            mapPrinter.PrintMap(String.Format(GameConstants.Header, antelopesCount, lionsCount), map);
        }

        /// <summary>
        /// Helper to create animal
        /// </summary>
        /// <param name="animalType">Type of animal</param>
        private void CreateAnimal(Type animalType)
        {
            var animal = animalCreationService.CreateAnimal(animalType, map);

            if (animal == null) return;

            lock (_lock) 
            {
                map.SetAnimal(animal);
            }
        }

        /// <summary>
        /// Helper to listen for key presses
        /// </summary>
        private void ListenForKeyPress()
        {
            while (_isRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.L:
                            CreateAnimal(typeof(Lion));
                            break;
                        case ConsoleKey.A:
                            CreateAnimal(typeof(Antelope));
                            break;
                    }
                }
            }
        }
    }
}
