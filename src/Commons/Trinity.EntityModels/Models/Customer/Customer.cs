using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Customer : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public User User { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Phone, MaxLength(15)]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

        // Navigation properties
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
