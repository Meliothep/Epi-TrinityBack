using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels.Models
{
    public sealed class Cart : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required Guid User { get; set; }

        [Required]
        public required ICollection<CartItem> CartItems { get; set;}
    }
}
