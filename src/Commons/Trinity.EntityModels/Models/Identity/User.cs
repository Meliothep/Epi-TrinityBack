using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class User : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Phone, MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
