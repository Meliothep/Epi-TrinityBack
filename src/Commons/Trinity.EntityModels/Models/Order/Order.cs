using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class Order : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Guid Customer { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public required ICollection<OrderItem> OrderItems { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public required CustomerAddress ShippingAddressId { get; set; }

        public required CustomerAddress BillingAddressId { get; set; }
    }
}
