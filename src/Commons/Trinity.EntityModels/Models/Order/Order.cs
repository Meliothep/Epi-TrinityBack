using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Order : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public decimal TotalAmount { get; set; }

        public int ShippingAddressId { get; set; }

        public int BillingAddressId { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
