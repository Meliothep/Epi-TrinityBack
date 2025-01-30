namespace Trinity.EntityModels.MessageModels
{
    public class CartUpdateRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}