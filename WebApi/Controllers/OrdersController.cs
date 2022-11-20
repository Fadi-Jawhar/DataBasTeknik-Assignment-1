using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateModel model)
        {
            var orderEntity = new OrderEntity
            {
                OrderDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                TotalPrice= model.TotalPrice,
                CustomerName = model.CustomerName,
                CustomerAddress = model.CustomerAddress
            };
            _context.Add(orderEntity);
            await _context.SaveChangesAsync();

            foreach(var product in model.Products)
            {
                _context.OrderRows.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,                                    
                });
                await _context.SaveChangesAsync();
            }

            return new OkResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = new List<OrderModel>();
            foreach (var order in await _context.Orders.Include(x => x.OrderRows).ToListAsync())
            {
                orders.Add(new OrderModel
                {                  
                    CustomerName = order.CustomerName,               
                    CustomerAddress = order.CustomerAddress,
                    TotalPrice = order.TotalPrice,
                    OrderRows = order.OrderRows
                });
            }

            return new OkObjectResult(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var orderEntity = await _context.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.Id == id);
            if (orderEntity != null)
            {
                return new OkObjectResult(new OrderCreateModel
                {
                    CustomerName = orderEntity.CustomerName,               
                    CustomerAddress = orderEntity.CustomerAddress,
                    TotalPrice = orderEntity.TotalPrice,
                    OrderRows = orderEntity.OrderRows
                });
            }
            return new NotFoundResult();
        }
    }
}
