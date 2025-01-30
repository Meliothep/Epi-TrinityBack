using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Invoice : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Order Order { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    }
}

