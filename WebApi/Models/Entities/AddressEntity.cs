namespace WebApi.Models.Entities
{
    public class AddressEntity
    {
        public int Id { get; set; }
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public int? StreetNumber { get; set; }
        public ICollection<CustomerEntity> Customers { get; set; } = null!;
    }
}
