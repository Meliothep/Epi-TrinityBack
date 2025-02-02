using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public Brand Brand { get; set; }

        [Required]
        public Category Category { get; set; } 

        [MaxLength(50)]
        public string? Barcode { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

        public string? ImageUrl { get; set; }

        public string? OffId { get; set; }

        public string? NutritionalInfo { get; set; } // JSON stored as string
    }
}
