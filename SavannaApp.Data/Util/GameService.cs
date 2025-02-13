using GameOfLife.Data.Interfaces.Game;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Util
{
    public class GameService(IMapCreator mapCreator, IMapPrinter mapPrinter, IAnimalCreationService animalCreationService) : IGameService
    {
        private IMap map = null!;
        private bool _isRunning = false;
        public void Execute()
        {
            map = mapCreator.CreateMap();

            _isRunning = true;


            Task.Run(ListenForKeyPress);

            RunLoop();
        }

        private void RunLoop()
        {
            while (_isRunning) 
            {
                var animalsList = map.Animals.ToList();

                foreach (var animal in animalsList) 
                {
                    animal.Move(map);
                    Thread.Sleep(10);
                }

                Print();

                Thread.Sleep(1000);
            }
        }

        private void Print()
        {
            mapPrinter.PrintMap("Welcome to Savanna", map);
        }

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
                            animalCreationService.CreateAnimal(typeof(Lion), map);
                            break;
                        case ConsoleKey.A:
                            animalCreationService.CreateAnimal(typeof(Antelope), map);
                            break;
                    }
                }
            }
        }
    }
}
