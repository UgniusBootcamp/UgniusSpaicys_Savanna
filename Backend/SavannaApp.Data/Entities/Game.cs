using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Entities
{
    public class Game
    {
        public string UserId { get; set; } = null!;
        public int Iteration { get; set; } = 0;
        public IMap Map { get; set; } = null!;
    }
}
