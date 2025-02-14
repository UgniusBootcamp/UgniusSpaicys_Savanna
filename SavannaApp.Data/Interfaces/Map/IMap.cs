using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Interfaces.Game
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }

        public List<Animal> Animals { get; }

        /// <summary>
        /// Method to set animal in map
        /// </summary>
        /// <param name="animal">animal to set</param>
        public void SetAnimal(Animal animal);

        /// <summary>
        /// Animal to get from map
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Animal from map</returns>
        public Animal? GetAnimal(int x, int y);

        /// <summary>
        /// Method to check if position is valid in map
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>true if valid, false if not</returns>
        public bool IsPositionValid(int x, int y);

        /// <summary>
        /// Removes dead animals
        /// </summary>
        public void RemoveDeadAnimals();
    }
}
