using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Product
{
    public class HelperCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Descritpion { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
