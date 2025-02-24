using SavannaApp.Business.Interfaces;
using SavannaApp.Data.Constants;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Business.Services
{
    public class GameService(ICreatableMapper mapper, IAssemblyLoader assemblyLoader, IMapCreator mapCreator, IMapPrinter mapPrinter, IAnimalCreationService animalCreationService, IAnimalGroupManager animalGroupManager) : IGameService
    {
        private IMap map = null!;
        private bool _isRunning = false;
        private readonly object _lock = new object();
        private Dictionary<ConsoleKey, Type> AnimalTypesMap = null!;

        /// <summary>
        /// Method to run game
        /// </summary>
        public void Execute()
        {
            AnimalTypesMap = mapper.MapCreatables(assemblyLoader.LoadAnimalTypes());

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

                animalGroupManager.Reproduction(map);

                var animals = map.Animals.ToList();

                foreach (var prays in animals.Where(a => a is Pray))
                {
                    prays.Move(map);
                }


                foreach (var hunter in animals.Where(a => a is Hunter))
                {
                    hunter.Move(map);
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

            mapPrinter.PrintMap(String.Format(GameConstants.Header), map);
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
                    var key = Console.ReadKey(true);
                    if (AnimalTypesMap[key.Key] != null) CreateAnimal(AnimalTypesMap[key.Key]);
                }
            }
        }
    }
}
