using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
public class ShoppingCartController : Controller
    {
       
        public async Task<ActionResult> AddToCart(int id)
        {


            List<ShoppingCartItem> _prevCart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart"); // My saved shopping cart

            List<ShoppingCartItem> _shoppingCart = new List<ShoppingCartItem>();


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
                else // if  shopping cart contain the product increment quantity
                {
                    int index = _prevCart.IndexOf(_product);
                    _prevCart[index].quantity += 1;


                    HttpContext.Session.Remove("Cart");
                    HttpContext.Session.SetComplexData("Cart", _prevCart);
                }

            }


            HttpContext.Session.SetInt32("Count", Count());
            HttpContext.Session.SetInt32("TotalPrice", TotalPrice());

            return RedirectToAction("Index", "Products");


        }


        public IActionResult ViewCart()
        {
            List<ShoppingCartItem> data = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");
            return View(data);
        }


        public IActionResult Increment(int id)
        {

            List<ShoppingCartItem> _Cart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart"); // My saved shopping cart

            var _product = _Cart.Find(x => x.product.Id == id);

            int index = _Cart.IndexOf(_product);

            _Cart[index].quantity += 1;

            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetComplexData("Cart", _Cart);

            HttpContext.Session.Remove("Count");
            HttpContext.Session.SetInt32("Count", Count());


            HttpContext.Session.Remove("TotalPrice");
            HttpContext.Session.SetInt32("TotalPrice", TotalPrice());


            return RedirectToAction("ViewCart",_Cart);
        }


        public IActionResult Decrement(int id)
        {

            List<ShoppingCartItem> _Cart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart"); // My saved shopping cart

            var _product = _Cart.Find(x => x.product.Id == id);

            int index = _Cart.IndexOf(_product);

            if (_Cart[index].quantity > 1)
                _Cart[index].quantity -= 1;


            else
                _Cart.RemoveAt(index);

            //Check if the shopping cart is empty

            if(_Cart.Count>0)
            {
                HttpContext.Session.Remove("Cart");
                HttpContext.Session.SetComplexData("Cart", _Cart);

                HttpContext.Session.Remove("Count");
                HttpContext.Session.SetInt32("Count", Count());

                HttpContext.Session.Remove("TotalPrice");
                HttpContext.Session.SetInt32("TotalPrice", TotalPrice());

            }
            else
            {
                HttpContext.Session.Remove("Cart");
                HttpContext.Session.Remove("Count");
                HttpContext.Session.Remove("TotalPrice");
            }

            return RedirectToAction("ViewCart", _Cart);
        }

        public IActionResult Remove(int id)
        {

            List<ShoppingCartItem> _Cart = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart"); // My saved shopping cart

            var _product = _Cart.Find(x => x.product.Id == id);

            int index = _Cart.IndexOf(_product);

             _Cart.RemoveAt(index);

            HttpContext.Session.Remove("Cart");
            HttpContext.Session.SetComplexData("Cart", _Cart);

            HttpContext.Session.Remove("Count");
            HttpContext.Session.SetInt32("Count", Count());


            HttpContext.Session.Remove("TotalPrice");
            HttpContext.Session.SetInt32("TotalPrice", TotalPrice());

            return RedirectToAction("ViewCart", _Cart);
        }




        public int Count()
        {
            int count = 0;
            List<ShoppingCartItem> data = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");
            foreach (var item in data)
            {
                count = count + item.quantity;

            }


            return count;
        }


        public int TotalPrice()
        {
            int total = 0;
            List<ShoppingCartItem> data = HttpContext.Session.GetComplexData<List<ShoppingCartItem>>("Cart");
            foreach (var item in data)
            {
                total = total + Convert.ToInt32(item.product.Price * item.quantity);

            }


            return total;
        }


    }
}
