using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Category : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
