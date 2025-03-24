using SavannaApp.Data.Dto.Game;

namespace SavannaApp.Business.Interfaces.Web
{
    public interface IGameHubService
    {
        void CreateGame(string userId, GameCreateDto gameCreateDto);
        Task LoadGame(string userId, string gameId);
        Task SaveGame(string userId);
        void PauseGame(string userId);
        void ResumeGame(string userId);
        void CreateAnimal(string userId, int animalTypeId);
        void RemoveGame(string userId);
    }
}
