namespace SavannaApp.Data.Dto.Game
{
    public class MapReadDto
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public IEnumerable<AnimalReadDto> Animals { get; set; } = [];
    }
}
