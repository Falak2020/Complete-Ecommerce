using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.OrderModel;
using E_Commerce_Api.Models.OrderItemModel;
using E_Commerce_Api.Models.AddressModel;
using Api.Models.UserAddressModel;
using Newtonsoft.Json;
using Api.Models.OrderModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderModel>>> GetOrders()
        {
            var orders = new List<GetOrderModel>();


            foreach (var order in await _context.Orders.Include(x => x.User).Include(x => x.DeliveryType).ToListAsync())
            {
                var OrderItemsCollection = new List<GetOrderItemModel>();
                foreach (var item in await _context.OrderItems.Where(item => item.OrderId == order.Id).ToListAsync())
                {
                    if (item != null)
                    {
                        OrderItemsCollection.Add(new GetOrderItemModel
                        {
                            Id = item.Id,
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                          

                        });
                    }

                }

                var _deliveryAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).Where(x => x.Id == order.DeliveryAddressId).FirstOrDefaultAsync();
                var _invoiceAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).Where(x => x.Id == order.InvoiceAddressId).FirstOrDefaultAsync();

                orders.Add(new GetOrderModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    OurReference = order.OurReference,
                    Status = order.Status,
                    DeliveryTypeName = order.DeliveryType.Name,
                    DeliveryAddress = new GetAddressModel
                    {
                        Id = _deliveryAddress.Address.Id,
                        AddressLine = _deliveryAddress.Address.AddressLine,
                        ZipCode = _deliveryAddress.Address.ZipCode,
                        City = _deliveryAddress.Address.City

                    },
                    InvoiceAddress = new GetAddressModel
                    {
                        Id = _invoiceAddress.Address.Id,
                        AddressLine = _invoiceAddress.Address.AddressLine,
                        ZipCode = _invoiceAddress.Address.ZipCode,
                        City = _invoiceAddress.Address.City

                    },
                    User = new GetOrdersUserModel
                    {
                        Id = order.User.Id,
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Email = order.User.Email,


                    },

                    OrderItems = OrderItemsCollection

                });
            }
                    return orders;
        
         }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOneOrderModel>> GetOrderEntity(int id)
        {
            var _order = await _context.Orders.Include(x=>x.DeliveryType).Include(x=>x.OrderItems).FirstOrDefaultAsync(x=> x.Id == id);
         
            if (_order == null)
            {
                return NotFound();
            }

            var OrderItemsCollection = new List<GetOrderItemModel>();
            foreach (var item in await _context.OrderItems.Include(x => x.Product).Where(item => item.OrderId == _order.Id).ToListAsync())
            {
                if (item != null)
                {
                    OrderItemsCollection.Add(new GetOrderItemModel
                    {
                        Id = item.Id,
                        OrderId = _order.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        product = item.Product

                    });
                }

            }
            return new GetOneOrderModel { 
            OrderDate = _order.OrderDate,
            OurReference = _order.OurReference,
            Status = _order.Status,
            DeliveryTypeName = _order.DeliveryType.Name,
            OrderItems = OrderItemsCollection
            
            };
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderEntity(int id,EditModel model)
        {
         
            var _order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

           
            _order.InvoiceAddressId = model.InvoiceAddressId;
            _order.DeliveryAddressId = model.DeliveryAddressId;
            _order.Status = model.Status;
            _order.DeliveryTypeId = model.DeliveryTypeId;

            _context.Entry(_order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderEntityExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderEntity>> PostOrderEntity(CreateOrderModel model)
        {
            var DeliveryName = await _context.DeliveryTypes.Where(x => x.Id == model.DeliveryTypeId).FirstOrDefaultAsync();
            var User = await _context.Users.Where(x => x.Id == model.UserId).FirstOrDefaultAsync();
            var _userAddresses = new List<GetUserAddressModel>();

            var _deliveryAddress = await _context.UserAddresses.Include(x => x.Address).Include(x=>x.User).Where(x => x.Id == model.DeliveryAddressId).FirstOrDefaultAsync();
            var _invoiceAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).Where(x => x.Id == model.InvoiceAddressId).FirstOrDefaultAsync();


           
          
            if (_deliveryAddress != null && _invoiceAddress != null && User != null)
            {
                if(_deliveryAddress.UserId == model.UserId && _invoiceAddress.UserId == model.UserId)
                {
                    var order = new OrderEntity
                        {
                            OrderDate = model.OrderDate,
                            Status = model.Status,
                            OurReference = model.OurReference,
                            UserId = model.UserId,
                            DeliveryTypeId = model.DeliveryTypeId,
                            DeliveryAddressId = model.DeliveryAddressId,
                            InvoiceAddressId = model.InvoiceAddressId,
                        };

                        _context.Orders.Add(order);
                        await _context.SaveChangesAsync();

                        return CreatedAtAction("GetOrderEntity", new { id = order.Id }, order);
                }
                else return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "The delivery address or the invoice address doesnot belong to the current user" }));

            }
            else
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "The delivery address or the invoice address or the User doesnot existed" }));
        }



        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderEntity(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

// Get all addresses which the user have
        private  async Task<List<GetUserAddressModel> >GetUsersAddresses( int id)
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


        // Get all orders which belong to one user
        [HttpGet("UserOrder/{userId}")]
        public async Task<ActionResult<List<GetOrderModel>>> UserOrders(int userId)
        {
            var orders = new List<GetOrderModel>();

            foreach (var order in await _context.Orders.Include(x => x.User).Include(x => x.DeliveryType).Where(x=> x.UserId == userId).ToListAsync())
            {
                var OrderItemsCollection = new List<GetOrderItemModel>();
                foreach (var item in await _context.OrderItems.Where(item => item.OrderId == order.Id).ToListAsync()) //Bring all order Items for one order
                {
                    if (item != null)
                    {
                        OrderItemsCollection.Add(new GetOrderItemModel
                        {
                            Id = item.Id,
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity

                        });
                    }

                }

                var _deliveryAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).Where(x => x.Id == order.DeliveryAddressId).FirstOrDefaultAsync();
                var _invoiceAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).Where(x => x.Id == order.InvoiceAddressId).FirstOrDefaultAsync();

                orders.Add(new GetOrderModel
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    OurReference = order.OurReference,
                    Status = order.Status,
                    DeliveryTypeName = order.DeliveryType.Name,
                    DeliveryAddress = new GetAddressModel
                    {
                        Id = _deliveryAddress.Address.Id,
                        AddressLine = _deliveryAddress.Address.AddressLine,
                        ZipCode = _deliveryAddress.Address.ZipCode,
                        City = _deliveryAddress.Address.City

                    },
                    InvoiceAddress = new GetAddressModel
                    {
                        Id = _invoiceAddress.Address.Id,
                        AddressLine = _invoiceAddress.Address.AddressLine,
                        ZipCode = _invoiceAddress.Address.ZipCode,
                        City = _invoiceAddress.Address.City

                    },
                    User = new GetOrdersUserModel
                    {
                        Id = order.User.Id,
                        FirstName = order.User.FirstName,
                        LastName = order.User.LastName,
                        Email = order.User.Email,


                    },

                    OrderItems = OrderItemsCollection

                });
            }
            return orders;
        }       


    }
}
