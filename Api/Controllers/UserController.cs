using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using Newtonsoft.Json;
using E_Commerce_Api.Models.UserModel;
using System.Text.RegularExpressions;
using E_Commerce_Api.Models.OrderItemModel;
using Api.Models.UserAddressModel;
using E_Commerce_Api.Models.AddressModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlContext _context;

        public UserController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserModel>>> GetUsers()
        {
            var users = new List<GetUserModel>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                var orders = new List<GetUsersOrdersModel>();

                foreach (var order in await _context.Orders.Include(x => x.DeliveryType).Where(x => x.UserId == user.Id).ToListAsync())
                {
                    var OrderItemsCollection = new List<GetOrderItemModel>();

                    OrderItemsCollection = await Task.Run(() => GetOrderItems(order.Id));

                    orders.Add(new GetUsersOrdersModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            OurReference = order.OurReference,
                            Status = order.Status,
                            DeliveryTypeName = order.DeliveryType.Name,
                            OrderItems = OrderItemsCollection

                        });
                 }


                var userAddresses = new List<GetUserAddressModel>();
                userAddresses = await Task.Run(() => GetUsersAddresses(user.Id));

                users.Add(new GetUserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserAddresses = userAddresses,
                    Orders = orders
                });

            }

            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserModel>> GetUserEntity(int id)
        {
            var _user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (_user == null)
            {
                return NotFound();
            }
            else
            {
                var _userAddresses = new List<GetUserAddressModel>();
                _userAddresses = await Task.Run(() => GetUsersAddresses(_user.Id));



                return new GetUserModel
                {
                    Id = _user.Id,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    Email = _user.Email,
                    UserAddresses = _userAddresses


                };

            }
           
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEntity(int id, UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserEntity>> PostUserEntity(CreateUserModel model)
        {
            var _user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            string PasseordError;

            var PasswordValidate = ValidatePassword(model.password, out PasseordError);

            if (_user == null)
            {
                if (IsValidEmail(model.Email))
                {
                    if (PasswordValidate)
                    {
                        var user = new UserEntity
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PasswordHash = new PasswordHashEntity
                            {
                                Password = model.password
                            }

                        };

                        _context.Users.Add(user);
                        await _context.SaveChangesAsync();

                        return CreatedAtAction("GetUserEntity", new { id = user.Id }, user);
                    }


                    return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = PasseordError }));
                }
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "Please enter a valid Email" }));
            }

            else
                return new ConflictResult();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEntity(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        //Validate Email
        bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }

        }

        //Validate Password

        private bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be less than or greater than 12 characters";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain At least one special case characters";
                return false;
            }
            else
            {
                return true;
            }

        }

        // Get User Adresses
        private async Task<List<GetUserAddressModel>> GetUsersAddresses(int id)
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

        // Get Order Items by entering order id
        private async Task<List<GetOrderItemModel>> GetOrderItems(int id)
        {
            var OrderItemsCollection = new List<GetOrderItemModel>();

            foreach (var item in await _context.OrderItems.Where(item => item.OrderId == id).ToListAsync())
            {
                OrderItemsCollection.Add(new GetOrderItemModel
                {
                    Id = item.Id,
                    OrderId = id,
                    ProductId = item.ProductId,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity

                });
            }

            return OrderItemsCollection;

        }
        //End 

     }
}
