using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Address : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Customer customer { get; set; }

        [Required]
        [MaxLength(200)]
        public string Street { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }
    }
}
