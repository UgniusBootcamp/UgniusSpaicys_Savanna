using SavannaApp.Data.Constants;
using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities.Animals
{
    public class Map : IMap
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public List<Animal> Animals { get; private set; } = new List<Animal>();

        /// <summary>
        /// Map constructor
        /// </summary>
        /// <param name="height">Height of Map</param>
        /// <param name="width">Width of Map</param>
        /// <exception cref="ArgumentException">If arguments are negative or equal to zero</exception>
        public Map(int height, int width)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException(GameConstants.InvalidPosition);


            Height = height;
            Width = width;
        }

        /// <summary>
        /// Add animal to map
        /// </summary>
        /// <param name="animal">aniaml to add</param>
        /// <exception cref="ArgumentException">If position is outside the boundries of a map or animal is already in that position</exception>
        public void SetAnimal(Animal animal)
        {
            int x = animal.Position.X;
            int y = animal.Position.Y;

            if (x < 0 || y < 0 || x >= Width || y >= Height || Animals.Any(a => a.Position.X == x && a.Position.Y == y))
                throw new ArgumentException(GameConstants.InvalidPosition);

            Animals.Add(animal);
        }

        /// <summary>
        /// Get animal in position
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Animal in a position or null if there is no animal there</returns>
        /// <exception cref="ArgumentException">If outside map boundries</exception>
        public Animal? GetAnimal(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new ArgumentException(GameConstants.InvalidPosition);

            return Animals.FirstOrDefault(a => a.Position.X == x && a.Position.Y == y);
        }

        /// <summary>
        /// Check if position is in boundries and free
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>true if valid, otherwise false</returns>
        public bool IsPositionValid(int x, int y)
        {
            return !(x < 0 || y < 0 || x >= Width || y >= Height || Animals.Any(a => a.Position.X == x && a.Position.Y == y));
        }

        /// <summary>
        /// Removes all animals froma a map which are marked as dead
        /// </summary>
        public void RemoveDeadAnimals()
        {
            Animals.RemoveAll(a => !a.IsAlive);
        }
    }
}
