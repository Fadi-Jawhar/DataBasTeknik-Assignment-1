using System.Collections.ObjectModel;
using WebApi.Models.Entities;

namespace WebApi.Models
{
    public class OrderCreateModel
    {
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public ObservableCollection<ProductModel> Products { get; set; } = null!;
        public ICollection<OrderRowEntity> OrderRows { get; set; }
    }
}
