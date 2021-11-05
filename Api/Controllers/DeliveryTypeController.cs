using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.DeliveryTypeModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private readonly SqlContext _context;

        public DeliveryTypeController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryTypeEntity>>> GetDeliveryTypes()
        {
            return await _context.DeliveryTypes.ToListAsync();
        }

        // GET: api/DeliveryType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryTypeEntity>> GetDeliveryTypeEntity(int id)
        {
            var deliveryTypeEntity = await _context.DeliveryTypes.FindAsync(id);

            if (deliveryTypeEntity == null)
            {
                return NotFound();
            }

            return deliveryTypeEntity;
        }

        // PUT: api/DeliveryType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryTypeEntity(int id, DeliveryTypeEntity deliveryTypeEntity)
        {
            if (id != deliveryTypeEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(deliveryTypeEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryTypeEntityExists(id))
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

        // POST: api/DeliveryType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryTypeEntity>> PostDeliveryTypeEntity(CreateDeliveryTypeModel model)
        {
            var deliveryType = new DeliveryTypeEntity
            {
                Name = model.Name
            };
            _context.DeliveryTypes.Add(deliveryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeliveryTypeEntity", new { id = deliveryType.Id }, deliveryType);
        }

       
        
        // DELETE: api/DeliveryType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryTypeEntity(int id)
        {
            var deliveryTypeEntity = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryTypeEntity == null)
            {
                return NotFound();
            }

            _context.DeliveryTypes.Remove(deliveryTypeEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryTypeEntityExists(int id)
        {
            return _context.DeliveryTypes.Any(e => e.Id == id);
        }
    }
}
