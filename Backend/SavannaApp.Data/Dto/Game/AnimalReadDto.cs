namespace SavannaApp.Data.Dto.Game
{
    public class AnimalReadDto
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; } = null!;
        public string Species { get; set; } = null!;
        public AnimalFeaturesReadDto Features { get; set; } = new AnimalFeaturesReadDto();
    }
}
