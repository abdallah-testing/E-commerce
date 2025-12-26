using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Category
{
    public class HelperProduct
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int StockQuantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; }

        [Required]
        [MaxLength(50)]
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
