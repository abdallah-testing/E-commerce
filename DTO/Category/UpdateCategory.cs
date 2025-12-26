using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Category
{
    public class UpdateCategory
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descritpion { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
