using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGamesManager
    {
        void AddGame(Game game);
        void RemoveGame(string userId);
        void AddAnimal(string userId, Type animalType);
        bool GameExist(string userId);
        void PauseGame(string userId);
        void ResumeGame(string userId);
        Game? GetGame(string userId);
    }
}
