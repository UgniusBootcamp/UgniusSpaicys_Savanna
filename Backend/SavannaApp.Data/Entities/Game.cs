using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities
{
    public class Game
    {
        public string Id = null!;
        public string UserId { get; set; } = null!;
        public bool IsRunning { get; set; } = false;
        public int Iteration { get; set; } = 0;
        public IMap Map { get; set; } = null!;
    }
}
