using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class OrderItem : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Order Order { get; set; }
        
        [Required]
        public required Guid Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
