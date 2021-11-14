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

            return RedirectToAction("Index","Products");
            
        }

        public ActionResult SignOut()
        {
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Products");
        }



    }
}
