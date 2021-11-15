using E_Commerce_MVC.Models;
using E_Commerce_MVC.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class AutenticationController : Controller
    {


        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

      [HttpPost]
        public async Task<ActionResult> SignIn(LogIn model)
        {
            var http = new HttpClient();
           
             var result = await http.PostAsJsonAsync("https://localhost:44356/api/User/LogIn", model);

             var returnValue = await result.Content.ReadFromJsonAsync<UserModel>();


            HttpContext.Session.SetString("UserId",returnValue.Id.ToString());
            HttpContext.Session.SetString("UserName", returnValue.FirstName.ToString() +" "+ returnValue.LastName.ToString());

            if (HttpContext.Session.GetString("Referrer") != null)
            { 
              HttpContext.Session.Remove("Referrer");
              return RedirectToAction("Create","Order");
              

            }
                
           

             return RedirectToAction("Index","Products");
            
        }


        #region SignUp

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUp model)
        {
            var http = new HttpClient();
            

            var result = await http.PostAsJsonAsync("https://localhost:44356/api/User/Register", model);

            var returnValue = await result.Content.ReadFromJsonAsync<UserModel>();


            HttpContext.Session.SetString("UserId", returnValue.Id.ToString());
            HttpContext.Session.SetString("UserName", returnValue.FirstName.ToString() + " " + returnValue.LastName.ToString());

            // user address

            var address = new AddressModel
            {
                AddressLine = model.address.AddressLine,
                ZipCode = model.address.ZipCode,
                City = model.address.City
            };

            var _addressRes = await http.PostAsJsonAsync("https://localhost:44356/api/Address", address);
            var _address = await _addressRes.Content.ReadFromJsonAsync<AddressModel>();
            await http.PostAsJsonAsync("https://localhost:44356/api/UserAddress", 
                
                new UserAddress {
                    UserId = returnValue.Id,
                    AddressId = _address.Id
            });

          

            if (HttpContext.Session.GetString("Referrer") != null)
            {
                HttpContext.Session.Remove("Referrer");
                return RedirectToAction("Create", "Order");


            }



            return RedirectToAction("Index", "Products");

        }
        #endregion


        public ActionResult SignOut()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Products");
        }



    }
}
