using SavannaApp.Data.Entities;

namespace SavannaApp.Business.Interfaces
{
    public interface IGameUpdateInformer
    {
        /// <summary>
        /// Method to notify user with game update
        /// </summary>
        /// <param name="game">updated game</param>
        /// <returns>informed user with new update</returns>
        Task NotifyGameUpdated(Game game);
    }
}
