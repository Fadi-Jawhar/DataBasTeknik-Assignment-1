using WebApi.Models.Entities;

namespace WebApi.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
        public ICollection<OrderRowEntity>? OrderRows { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
    }
}
