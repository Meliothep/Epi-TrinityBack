using System;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Payment : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Invoice Invoice { get; set; }
    }
}
