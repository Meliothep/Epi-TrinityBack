using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Invoice : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Order Order { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}

