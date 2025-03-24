using SavannaApp.Data.Dto.Game.Save;
using SavannaApp.Data.Entities;
using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Business.Interfaces
{
    public interface IDtoMapper
    {
        public AnimalSaveDto? MapAnimalSave(Animal animal);
        public Animal? MapAnimal(AnimalSaveDto animal);
        public GameSaveDto MapGameSave(Game game);
        public Game MapGame(GameSaveDto gameSaveDto);
    }
}
