using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Category
{
    public class CreateCategory
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descritpion { get; set; }
        public bool IsActive { get; set; } = true;
        private DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
