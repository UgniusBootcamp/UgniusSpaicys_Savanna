using SavannaApp.Data.Interfaces;

namespace SavannaApp.Data.Dto.Game.Save
{
    public class GameSaveDto
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int Iteration { get; set; } = 0;
        public MapSaveDto Map { get; set; } = null!;
    }
}
