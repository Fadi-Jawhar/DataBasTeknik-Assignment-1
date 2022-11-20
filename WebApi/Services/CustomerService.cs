using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Services
{
    public class CustomerService
    {
       
            private readonly DataContext _context;

            public CustomerService(DataContext context)
            {
                _context = context;
            }

            public async Task Create(CustomerCreateModel model)
            {
            
            try 
            { 
                var customerEntity = new CustomerEntity
                {
                     Name = model.Name,
                     Email = model.Email,
                     Phone = model.Phone
                };
                    _context.Add(customerEntity);
                    await _context.SaveChangesAsync();
            }
             
            catch { }
            }

            public async Task<IEnumerable<CustomerModel>> GetAll()
            {
                var customers = new List<CustomerModel>();
                foreach (var customer in await _context.Customers.ToListAsync())
                    customers.Add(new CustomerModel { Id = customer.Id, Name = customer.Name, Email = customer.Email, Phone = customer.Phone });

                return customers;
            }

            public async Task<CustomerModel> Get(int id)
            {
                var customerEntity = await _context.Customers.FindAsync(id);
                if (customerEntity != null)
                    return new CustomerModel { Id = customerEntity.Id, Name = customerEntity.Name, Email = customerEntity.Email, Phone = customerEntity.Phone };

                return null!;
            }
        }
    
}
