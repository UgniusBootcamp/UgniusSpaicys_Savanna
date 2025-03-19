using System.ComponentModel.DataAnnotations;

namespace SavannaApp.Data.Dto.Game
{
    public class GameCreateDto
    {
        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }
    }
}
