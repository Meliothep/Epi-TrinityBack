using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Customer : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required User User { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public required string Email { get; set; }

        [Phone, MaxLength(15)]
        public required string PhoneNumber { get; set; }

        public string? Notes { get; set; }

        public required ICollection<Address> Addresses { get; set; }
        //public required ICollection<Order> Orders { get; set; }
    }
}
