using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.AddressModel;
using Api.Models.AddressModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly SqlContext _context;

        public AddressController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAddressModel>>> GetAddresses()
        {
            var addresses = new List<GetAddressModel>();
            foreach (var address in await _context.Addresses.ToListAsync())
            {
                addresses.Add(new GetAddressModel
                {
                    Id = address.Id,
                    AddressLine = address.AddressLine,
                    City = address.City,
                    ZipCode = address.ZipCode
                });
            }

            return addresses;
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressEntity>> GetAddressEntity(int id)
        {
            var addressEntity = await _context.Addresses.FindAsync(id);

            if (addressEntity == null)
            {
                return NotFound();
            }

            return addressEntity;
        }

        // PUT: api/Address/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressEntity(int id, AddressEntity addressEntity)
        {
            if (id != addressEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressEntityExists(id))
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

        // POST: api/Address
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressEntity>> PostAddressEntity(CreateAddressModel model)
        {
            var address = new AddressEntity
            {
                AddressLine = model.AddressLine,
                ZipCode = model.ZipCode,
                City = model.City
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressEntity", new { id = address.Id }, address);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressEntity(int id)
        {
            var addressEntity = await _context.Addresses.FindAsync(id);
            if (addressEntity == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addressEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressEntityExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
