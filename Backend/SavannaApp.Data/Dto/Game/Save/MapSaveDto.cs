namespace SavannaApp.Data.Dto.Game.Save
{
    public class MapSaveDto
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<AnimalSaveDto> Animals { get; set; } = [];
    }
}
