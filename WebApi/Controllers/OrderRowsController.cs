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
    public class OrderRowsController : ControllerBase
    {   
            private readonly DataContext _context;

            public OrderRowsController(DataContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var orderRows = new List<OrderRowModel>();
                foreach (var orderRow in await _context.OrderRows.Include(x => x.Product).ToListAsync())
                {
                    orderRows.Add(new OrderRowModel
                    {
                        Id = orderRow.Id,
                        OrderId = orderRow.OrderId,
                        OrderEntityId = orderRow.OrderEntityId,
                        ProductId = orderRow.ProductId,
                        ProductQuantitiy = orderRow.ProductQuantitiy,
                        Product = orderRow.Product
                    });
                }
                return new OkObjectResult(orderRows);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var orderRowEntity = await _context.OrderRows.FindAsync(id);
                if (orderRowEntity != null)
                {
                    return new OkObjectResult(new OrderRowModel
                    {
                        Id = orderRowEntity.Id,
                        OrderId = orderRowEntity.OrderId,
                        OrderEntityId = orderRowEntity.OrderEntityId,
                        ProductId = orderRowEntity.ProductId,
                        ProductQuantitiy = orderRowEntity.ProductQuantitiy,
                        Product = orderRowEntity.Product
                    });
                }
                return new NotFoundResult();
            }

            [HttpPost]
            public async Task<IActionResult> Create(OrderRowCreateModel model)
            {
                if (ModelState.IsValid)
                {
                    var orderRowEntity = new OrderRowEntity
                    {
                        OrderId = model.OrderId,
                        OrderEntityId = model.OrderEntityId,
                        ProductId = model.ProductId,
                        ProductQuantitiy = model.ProductQuantitiy

                    };
                    _context.Add(orderRowEntity);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                return BadRequest();
            }
    
    }   
}
