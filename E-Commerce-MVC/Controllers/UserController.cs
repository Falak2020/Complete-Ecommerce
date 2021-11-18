using E_Commerce_MVC.Models;
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
        public async Task<ActionResult> Create()
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
    }
}
