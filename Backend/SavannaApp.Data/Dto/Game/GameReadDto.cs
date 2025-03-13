namespace SavannaApp.Data.Dto.Game
{
    public class GameReadDto
    {
        public int Iteration { get; set; }
        public int AnimalCount { get; set; }
        public MapReadDto Map { get; set; } = null!;
    }
}
