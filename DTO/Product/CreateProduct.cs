using E_CommerceSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceSystem.DTO.Product
{
    public class CreateProduct
    {
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
        public bool IsActive { get; set; }

        private DateTime CreatedAt { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
    }
}
