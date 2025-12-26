using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Account
{
    public class Login
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
