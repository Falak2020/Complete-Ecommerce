using E_Commerce_MVC.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class AutenticationController : Controller
    {
        
    
        public ActionResult SignIn()
        {
            return View();
        }

       


    }
}
