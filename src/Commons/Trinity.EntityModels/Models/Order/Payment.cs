using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Payment : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Invoice Invoice { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;        
    }
}
