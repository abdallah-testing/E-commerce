using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
