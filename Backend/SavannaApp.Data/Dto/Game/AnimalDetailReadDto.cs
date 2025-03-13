namespace SavannaApp.Data.Dto.Game
{
    public class AnimalDetailReadDto
    {
        public string Species { get; set; } = null!;
        public AnimalFeaturesReadDto Features { get; set; } = new AnimalFeaturesReadDto();
    }
}
