using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class InvoiceItem : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
    }
}
