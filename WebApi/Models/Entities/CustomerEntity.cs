namespace WebApi.Models.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public int AddressId { get; set; }

        public AddressEntity Address { get; set; } = null!;
    }
}
