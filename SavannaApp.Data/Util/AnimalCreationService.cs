using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannaApp.Data.Entities.Animals;
using SavannaApp.Data.Interfaces;
using SavannaApp.Data.Interfaces.Game;

namespace SavannaApp.Data.Util
{
    public class AnimalCreationService(IAnimalFactory animalFactory) : IAnimalCreationService
    {
        private readonly Random random = new Random();

        public void CreateAnimal(Type animalType, IMap map)
        {
            var newPosition = GetRandomFreePlaceOnMap(map);

            if (newPosition == null) return;

            var newAnimal = animalFactory.CreateAnimal(animalType, newPosition.X, newPosition.Y);

            map.SetAnimal(newAnimal);
        }

        private Position? GetRandomFreePlaceOnMap(IMap map)
        {
            if (map.Animals.Count() == map.Height * map.Width) return null;

            int x;
            int y;

            Animal? animal = null;

            do
            {
                x = random.Next(0, map.Width);
                y = random.Next(0, map.Height);

                animal = map.GetAnimal(x, y);

            } while (animal != null);

            return new Position(x, y);
        }

    }
}
