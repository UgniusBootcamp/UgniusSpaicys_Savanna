namespace SavannaApp.Data.Dto.Game
{
    public class GameLoadInfoDto
    {
        public string Id { get; set; } = null!;
        public int Iteration { get; set; }
        public DateTime? LastModified { get; set; }
        public Dictionary<string, int> AnimalCount { get; set; } = null!;
    }
}
