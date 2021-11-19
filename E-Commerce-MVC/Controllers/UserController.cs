using E_Commerce_MVC.Models;
using E_Commerce_MVC.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class UserController : Controller
    {
       

        public async Task<ActionResult> GetAddress()
        {
            string userid = HttpContext.Session.GetString("UserId");
            var http = new HttpClient();
            var _userAddresses = await http.GetFromJsonAsync<List<UserAddress>>($"https://localhost:44356/api/UserAddress/AllAddress/{userid}");
            return View(_userAddresses);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AddressModel _addressModel)
        {
            var http = new HttpClient();
          
            var _addressRes = await http.PostAsJsonAsync("https://localhost:44356/api/Address", _addressModel);
            var _address = await _addressRes.Content.ReadFromJsonAsync<AddressModel>();
            await http.PostAsJsonAsync("https://localhost:44356/api/UserAddress",
               new UserAddress
               {
                   UserId = int.Parse(HttpContext.Session.GetString("UserId")),
                   AddressId = _address.Id
               });

            return RedirectToAction("GetAddress","User");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(LogIn model)
        {
            var http = new HttpClient();

            HttpResponseMessage hrm = await http.PutAsJsonAsync($"https://localhost:44356/api/Authentication/{HttpContext.Session.GetString("UserId")}",new LogIn { password = model.password});
            
           
            return RedirectToAction("Index","Products");
        }

        }
    }
