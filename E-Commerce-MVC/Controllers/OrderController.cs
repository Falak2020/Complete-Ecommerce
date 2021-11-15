using E_Commerce_MVC.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        
        
        
        
        
        
        
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public async Task<ActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserId") == null)
            {
                HttpContext.Session.SetString ("Referrer",System.IO.Path.GetFileName(Request.Path.ToString()));

                return RedirectToAction("SignIn", "Autentication");
            }


               

            else
            {
                List<SelectListItem> list = new List<SelectListItem>(); //save delivery types

                List<SelectListItem> Addresses = new List<SelectListItem>(); // save user addresses 
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

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
