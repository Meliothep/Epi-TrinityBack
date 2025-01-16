using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class CartItem : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Cart Cart { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
