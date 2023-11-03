using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTH.H23.PA2577.Assignment1.OrderService.Models;

namespace BTH.H23.PA2577.Assignment1.OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public OrderController(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders() => await _databaseContext.Orders.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var orderSet = _databaseContext
                .Orders
                .Where(p => p.Id == id)
                .Include(m => m.OrderLines);

            if (!orderSet.Any())
            {
                return NotFound();
            }
            var order = orderSet.First();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] Order order)
        {
            var entity = _databaseContext.Add(order);
            await _databaseContext.SaveChangesAsync();

            return Ok(entity.Entity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(Guid id, [FromBody] Order newOrder)
        {
            if (id != newOrder.Id)
            {
                return BadRequest();
            }

            DbSet<Order> orderSet = _databaseContext.Orders;

            var order = orderSet.First(p => p.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderDate = newOrder.OrderDate;
            order.OrderLines = newOrder.OrderLines;

            var entry = _databaseContext.Update(order);
            await _databaseContext.SaveChangesAsync(true);
            return Ok(entry.Entity);
        }

        // DELETE: /Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _databaseContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _databaseContext.Orders.Remove(order);
            await _databaseContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
