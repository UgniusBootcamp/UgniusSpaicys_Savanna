using SavannaApp.Data.Dto.Game.Save;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Interfaces
{
    public interface IDtoMapper
    {
        /// <summary>
        /// Animal to animal save dto map
        /// </summary>
        /// <param name="animal">animal</param>
        /// <returns>mapped animal save dto</returns>
        public AnimalSaveDto? MapAnimalSave(Animal animal);

        /// <summary>
        /// animal save dto to animal map
        /// </summary>
        /// <param name="animal">animal save dto</param>
        /// <returns>animal</returns>
        public Animal? MapAnimal(AnimalSaveDto animal);

        /// <summary>
        /// game map to game save dto
        /// </summary>
        /// <param name="game">game</param>
        /// <returns>game save dto</returns>
        public GameSaveDto MapGameSave(Game game);

        /// <summary>
        /// game save dto map to game
        /// </summary>
        /// <param name="gameSaveDto">game save dto</param>
        /// <returns>game</returns>
        public Game MapGame(GameSaveDto gameSaveDto);
    }
}
