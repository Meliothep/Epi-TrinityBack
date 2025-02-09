using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Trinity.EntityModels.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(8)]
        public string IdSupplier { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public List<Brand> Brands { get; set; } = new();

        [Required]
        public Guid MainCategoryId { get; set; }

        [Required]
        public List<Category> Categories { get; set; } = new();

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

        public string? Weight { get; set; }

        public string? ImageUrl { get; set; }

        public List<Origin> Origins { get; set; } = new List<Origin>();

        public List<Label> Labels { get; set; } = new List<Label>();

        public List<Allergen> Allergens { get; set; } = new List<Allergen>();

        [Column(TypeName = "jsonb")]
        public string? NutritionInfo { get; set; } // JSON stored as a string

        public char? NutritionGrade { get; set; }
    }
}
