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
        private readonly object _lock = new object();
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

                var animals = map.Animals.ToList();

                foreach (var animal in animals) 
                {
                    animal.Move(map);
                }

                lock (_lock)
                {
                    map.RemoveDeadAnimals();
                }

                Print();

                Thread.Sleep(1000);
            }
        }

        private void Print()
        {
            mapPrinter.PrintMap("Welcome to Savanna", map);
        }

        private void CreateAnimal(Type animalType)
        {
            var animal = animalCreationService.CreateAnimal(animalType, map);

            if (animal == null) return;

            lock (_lock) 
            {
                map.SetAnimal(animal);
            }
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
