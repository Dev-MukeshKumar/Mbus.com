using System.ComponentModel.DataAnnotations;

namespace Mbus.com.Models
{
    public class OwnerLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
