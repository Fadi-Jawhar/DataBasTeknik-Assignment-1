using WebApi.Models.Entities;

namespace WebApi.Models
{
    public class OrderRowCreateModel
    {
        public int ProductQuantitiy { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int OrderEntityId { get; set; }
        public ProductEntity Product { get; set; } = null!;
    }
}
