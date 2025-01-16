using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Brand : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; }
    }
}
