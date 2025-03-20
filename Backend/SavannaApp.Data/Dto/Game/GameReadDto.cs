namespace SavannaApp.Data.Dto.Game
{
    public class GameReadDto
    {
        public int Iteration { get; set; }
        public bool IsRunning { get; set; }
        public Dictionary<string, int> AnimalCount { get; set; } = null!;
        public MapReadDto Map { get; set; } = null!;
    }
}
