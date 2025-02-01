using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Brand : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        // Navigation property
        public required ICollection<Product> Products { get; set; }
    }
}
