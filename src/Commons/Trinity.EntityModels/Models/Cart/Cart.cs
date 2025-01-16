using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinity.EntityModels
{
    public class Cart : BaseEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<CartItem> CartItems { get;}
    }
}
