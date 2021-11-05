using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.OrderItemModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderItemController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemEntity>>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }

        // GET: api/OrderItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemEntity>> GetOrderItemEntity(int id)
        {
            var orderItemEntity = await _context.OrderItems.FindAsync(id);

            if (orderItemEntity == null)
            {
                return NotFound();
            }

            return orderItemEntity;
        }

        // PUT: api/OrderItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItemEntity(int id, OrderItemEntity orderItemEntity)
        {
            if (id != orderItemEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItemEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderItemEntity>> PostOrderItemEntity(CreateOrderItemModel model)
        {
            var _product = await _context.Products.Where(x => x.Id == model.ProductId).FirstOrDefaultAsync();
            var orderItem = new OrderItemEntity
            {
                ProductId = model.ProductId,
                OrderId = model.OrderId,
                Quantity = model.Quantity,
                UnitPrice = _product.Price
            };


            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItemEntity", new { id = orderItem.Id }, orderItem);
        }

        // DELETE: api/OrderItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItemEntity(int id)
        {
            var orderItemEntity = await _context.OrderItems.FindAsync(id);
            if (orderItemEntity == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItemEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemEntityExists(int id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
