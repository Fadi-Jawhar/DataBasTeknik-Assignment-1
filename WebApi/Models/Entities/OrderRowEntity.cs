namespace WebApi.Models.Entities
{
    public class OrderRowEntity
    {
        public int Id { get; set; }
        public int ProductQuantitiy { get; set; }
        public int ProductId { get; set; }      
        public int OrderId { get; set; }
        public int OrderEntityId { get; set; }
        public ProductEntity Product { get; set; } = null!;
        public OrderEntity Order { get; set; } = null!;
    }
}
