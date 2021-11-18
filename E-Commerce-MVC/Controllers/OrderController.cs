using E_Commerce_MVC.Controllers;
using E_Commerce_MVC.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class OrderController : Controller
    {
        //Get all order för en spsific user
        public async Task<ActionResult> Index()
        {
            var http = new HttpClient();

            var _orders = await http.GetFromJsonAsync<List<MyOrder>>($"https://localhost:44356/api/Order/UserOrder/{HttpContext.Session.GetString("UserId") }");

            if (_orders.Count > 0)
                return View(_orders);
            else
                return View(null);
        }

        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {

            var uri = Request.Path.Value;

            if (HttpContext.Session.GetString("UserId") == null)
            {
               // HttpContext.Session.SetString ("Referrer",uri);

                return RedirectToAction("SignIn", "Autentication", new {ReturnUrl = uri });
            }       

            else
            {
                List<SelectListItem> list = new List<SelectListItem>(); //save delivery types

                List<SelectListItem> Addresses = new List<SelectListItem>(); // save user addresses one user can has two addresses
                var http = new HttpClient();

                var _deliveryType = await http.GetFromJsonAsync<List<DeliveryType>>("https://localhost:44356/api/DeliveryType");

                var _address = await http.GetFromJsonAsync<List<UserAddress>>($"https://localhost:44356/api/UserAddress/AllAddress/{HttpContext.Session.GetString("UserId") }");


                foreach (var type in _deliveryType)
                {
                    list.Add(new SelectListItem
                    {
                        Text = type.Name,
                        Value = type.Id.ToString()
                    });

                }

                foreach (var address in _address)
                {
                    Addresses.Add(new SelectListItem
                    {
                        Text = address.Address.AddressLine+address.Address.ZipCode+address.Address.City,
                        Value = address.Id.ToString()
                    });

                }

                ViewData["DeliveryType"] = list;

                ViewData["Addresses"] = Addresses;

                return View();

            }           
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Order order)
        {
            try
            {
                var _order = new Order
                {
                    OrderDate = order.OrderDate,
                    OurReference = order.OurReference,
                    Status = order.Status,
                    DeliveryAddressId = order.DeliveryAddressId,
                    InvoiceAddressId = order.InvoiceAddressId,
                    UserId = int.Parse(HttpContext.Session.GetString("UserId")),
                    DeliveryTypeId = order.DeliveryTypeId
                };

                var client = new HttpClient();
                var response =  await client.PostAsJsonAsync("https://localhost:44356/api/Order",_order);

                var returnValue = await response.Content.ReadFromJsonAsync<Order>();

                int _orderId = returnValue.Id;

                List<ShoppingCartItem> ShoppingCart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");

                foreach ( var item in ShoppingCart)
                {
                    var _orderItem = new OrderItem
                    {
                        OrderId = _orderId,
                        ProductId = item.product.Id,
                        Quantity = item.quantity,
                        UnitPrice = item.product.Price
                    };

                    await client.PostAsJsonAsync("https://localhost:44356/api/OrderItem", _orderItem);

                }

                HttpContext.Session.Remove("Cart");
                HttpContext.Session.Remove("Count");
                HttpContext.Session.Remove("TotalPrice");

            return RedirectToAction("Index" ,"Products");
              
            }
            catch
            {
                return View(order);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var http = new HttpClient();

            var _order = await http.GetFromJsonAsync<MyOrder>($"https://localhost:44356/api/Order/{id}");

            return View(_order);

        }


        public async Task<ActionResult> Delete(int id)
        {
            var http = new HttpClient();

             await http.DeleteAsync($"https://localhost:44356/api/Order/{id}");

            return RedirectToAction("Index");

        }
    }
}
