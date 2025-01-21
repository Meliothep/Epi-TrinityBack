using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public sealed class User : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Customer;
    }
}
