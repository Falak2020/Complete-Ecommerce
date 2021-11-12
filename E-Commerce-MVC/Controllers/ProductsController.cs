using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;
using Microsoft.PowerBI.Api.Models;

namespace E_Commerce_MVC.Controllers
{
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }

    public class ProductsController : Controller
    {
     

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var http = new HttpClient();

            var products = await http.GetFromJsonAsync<List<Product>>("https://localhost:44356/api/Product");
            return View(products);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var http = new HttpClient();

            var product = await http.GetFromJsonAsync<Product>($"https://localhost:44356/api/Product/{id}");
            return View(product);
           
        }

        // GET: ProductsController/Create
        public async Task <ActionResult> Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var http = new HttpClient();

            var subCategories = await http.GetFromJsonAsync<List<SubCategory>>("https://localhost:44356/api/SubCategory");
            
           foreach(var cat in subCategories)
            {
                list.Add(new SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                });

            }
           
               

                    ViewData["SubCategory"] =list;

            return View();
          
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)

        {

            try
            {
                var client = new HttpClient();
                await client.PostAsJsonAsync("https://localhost:44356/api/Product", product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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

        public async Task<ActionResult> AddToCart( int id)
        {
            
            
            List<ShoppingCartItem> _prevCart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart"); // My saved shopping cart

           List<ShoppingCartItem> _shoppingCart =new  List<ShoppingCartItem>() ;

            
            var http = new HttpClient();

            var product = await http.GetFromJsonAsync<Product>($"https://localhost:44356/api/Product/{id}");

            if (_prevCart == null)
            {
                
                _shoppingCart.Add(new ShoppingCartItem
                {
                    product = product,
                    quantity = 1
                });
             
               HttpContext.Session.SetComplexData("Cart", _shoppingCart);
               

            }
            else
            {
                var _product = _prevCart.Find(x => x.product.Id == id);

                if (_product == null) //if  shopping cart doesnot contain the product
                {

                    _prevCart.Add(new ShoppingCartItem
                    {
                        product = product,
                        quantity = 1

                        
                    });
                    

                    HttpContext.Session.Remove("Cart");
                    HttpContext.Session.SetComplexData("Cart", _prevCart);

                }
                else // if  shopping cart contain the product
                {
                    int index = _prevCart.IndexOf(_product);
                    _prevCart[index].quantity += 1;


                    HttpContext.Session.Remove("Cart");
                    HttpContext.Session.SetComplexData("Cart", _prevCart);
                }

            }









            /*   var http = new HttpClient();

                var orderItems = await http.GetFromJsonAsync<List<Orderitem>>($"https://localhost:44356/api/OrderItem");

                var _item = orderItems.Find(x => x.ProductId == id);

                if(_item == null)
                {
                    var _cart = new Orderitem
                        {
                           ProductId = id,
                           OrderId = 1,
                           Quantity = 1,

                        };
                    await http.PostAsJsonAsync("https://localhost:44356/api/OrderItem", _cart);
                }

                else
                {
                    var _cart = new Orderitem
                    {
                        ProductId = id,
                        OrderId = 1,
                        Quantity = 1,

                    };

                    await http.PutAsJsonAsync($"https://localhost:44356/api/OrderItem/{_item.Id}",_cart);

                }

            */
            HttpContext.Session.SetInt32("Count", Count());
            return RedirectToAction("Index", "Products");


        }
        public IActionResult ViewCart()
        {
            List<ShoppingCartItem> data = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");
            return View(data);
        }


        public int Count()
        {
            int count = 0;
            List<ShoppingCartItem> data = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");
            foreach(var item in data)
            {
                count = count + item.quantity;

            }

           
            return count;
        }


    }
    }
