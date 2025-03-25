using SavannaApp.Data.Entities.Animals;

namespace SavannaApp.Data.Dto.Game.Save
{
    public class AnimalSaveDto
    {
        public Position Position { get; set; } = null!;
        public AnimalFeatures Features { get; set; } = null!;
        public int AnimalTypeId { get; set; }
    }
}
