using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public class CartItem : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Cart Cart { get; set; }

        [Required]
        public required Guid Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
