using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class CustomerAddress : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required, MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(20)]
        public string ZipCode { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        public bool IsBilling { get; set; } = false;

        public bool IsShipping { get; set; } = false;

        // Navigation properties
        public Customer Customer { get; set; }
    }
}
