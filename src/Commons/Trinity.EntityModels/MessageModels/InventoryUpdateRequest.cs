namespace Trinity.EntityModels.MessageModels
{
    public class InventoryUpdateRequest
    {
        public int Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}