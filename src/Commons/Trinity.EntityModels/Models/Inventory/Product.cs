using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public required Brand Brand { get; set; }

        [Required]
        public required Category Category { get; set; } 

        [MaxLength(50)]
        public string? Barcode { get; set; }

        [Required]
        public required decimal Price { get; set; }

        public required int StockQuantity { get; set; } = 0;

        public string? ImageUrl { get; set; }

        public string? OffId { get; set; }

        public string? NutritionalInfo { get; set; } // JSON stored as string
    }
}
