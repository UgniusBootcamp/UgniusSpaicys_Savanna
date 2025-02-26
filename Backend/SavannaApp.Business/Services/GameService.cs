using System.Text;
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
        private string _header = string.Empty;

        /// <summary>
        /// Method to run game
        /// </summary>
        public void Execute()
        {
            AnimalTypesMap = mapper.MapCreatableAnimals(assemblyLoader.LoadAnimalTypes());
            SetHeader();

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
            var footer = new StringBuilder(GameConstants.MapVerticalBorder);

            foreach(var type in AnimalTypesMap.Values)
            {
                var count = map.Animals.Where(a => a.GetType() == type).Count();
                var message = String.Format(" {0} {1} {2}", type.Name, count, GameConstants.MapVerticalBorder);
                footer.Append(message);
            }

            mapPrinter.PrintMap(_header, footer.ToString(), map);
        }

        /// <summary>
        /// Method for initializing header
        /// </summary>
        private void SetHeader()
        {
            var header = new StringBuilder(GameConstants.MapVerticalBorder);

            foreach(var key in AnimalTypesMap.Keys)
            {
                var type = AnimalTypesMap[key];
                var message = String.Format(" {0} - {1} {2}", type.Name, key.ToString(), GameConstants.MapVerticalBorder);
                header.Append(message);
            }

            _header = header.ToString();
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
                    if (AnimalTypesMap.ContainsKey(key.Key)) CreateAnimal(AnimalTypesMap[key.Key]);
                }
            }
        }
    }
}
