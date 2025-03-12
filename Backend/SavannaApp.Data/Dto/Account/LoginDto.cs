using System.ComponentModel.DataAnnotations;

namespace SavannaApp.Data.Dto.Account
{
    public class LoginDto
    {
        [Required]
        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
