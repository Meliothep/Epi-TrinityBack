using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class CustomerAddress : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public required string City { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Country { get; set; }
    }
}
