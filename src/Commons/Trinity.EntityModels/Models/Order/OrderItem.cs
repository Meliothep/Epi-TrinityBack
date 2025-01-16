using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class OrderItem : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Order Order { get; set; }
        

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
