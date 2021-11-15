using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using Api.Models.UserAddressModel;
using Newtonsoft.Json;
using E_Commerce_Api.Models.AddressModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly SqlContext _context;

        public UserAddressController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/UserAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserAddressModel>>> GetUserAddresses()
        {
            var userAddresses = new List<GetUserAddressModel>();
            foreach (var userAddress in await _context.UserAddresses.ToListAsync())
                userAddresses.Add(new GetUserAddressModel
                {
                    Id = userAddress.Id,
                    UserId = userAddress.UserId,
                    AddressId = userAddress.AddressId
                });
            return userAddresses;
        }

        // GET: api/UserAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAddressEntity>> GetUserAddressEntity(int id)
        {
            var userAddressEntity = await _context.UserAddresses.FindAsync(id);

            if (userAddressEntity == null)
            {
                return NotFound();
            }

            return userAddressEntity;
        }

        // PUT: api/UserAddress/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAddressEntity(int id, UserAddressEntity userAddressEntity)
        {
            if (id != userAddressEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAddressEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAddressEntityExists(id))
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

        // POST: api/UserAddress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAddressEntity>> PostUserAddressEntity(CreateUserAddressModel model)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            var _address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == model.AddressId);

            if (_user != null && _address != null)
            {
                var userAddress = new UserAddressEntity
                {
                    AddressId = model.AddressId,
                    UserId = model.UserId
                };

                _context.UserAddresses.Add(userAddress);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUserAddressEntity", new { id = userAddress.Id }, userAddress);

            }

            else
                return BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "Please enter a valid userId and  addressId" }));

           
        }

        private ActionResult<UserAddressEntity> BadRequestObjectResult(string v)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/UserAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAddressEntity(int id)
        {
            var userAddressEntity = await _context.UserAddresses.FindAsync(id);
            if (userAddressEntity == null)
            {
                return NotFound();
            }

            _context.UserAddresses.Remove(userAddressEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAddressEntityExists(int id)
        {
            return _context.UserAddresses.Any(e => e.Id == id);
        }

        // return  all addresses which the user has


        [HttpGet("/api/UserAddress/AllAddress/{id}")]
        public async Task<List<GetUserAddressModel>> GetUsersAddresses(int id)
        {
            var _userAddresses = new List<GetUserAddressModel>();
            foreach (var address in await _context.UserAddresses.Where(x => x.UserId == id).Include(x => x.Address).ToListAsync())
            {
                _userAddresses.Add(new GetUserAddressModel
                {
                    Id = address.Id,
                    UserId = address.UserId,
                    AddressId = address.AddressId,
                    Address = new GetAddressModel
                    {
                        Id = address.Address.Id,
                        AddressLine = address.Address.AddressLine,
                        ZipCode = address.Address.ZipCode,
                        City = address.Address.City
                    }
                });
            }

            return _userAddresses;

        }
    }
}
