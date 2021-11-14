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

                return RedirectToAction("SignIn", "Autentication");

            else
            {
                List<SelectListItem> list = new List<SelectListItem>();
                var http = new HttpClient();

                var _deliveryType = await http.GetFromJsonAsync<List<DeliveryType>>("https://localhost:44356/api/DeliveryType");

                foreach (var type in _deliveryType)
                {
                    list.Add(new SelectListItem
                    {
                        Text = type.Name,
                        Value = type.Id.ToString()
                    });

                }



                ViewData["DeliveryType"] = list; 
                return View();

            }
           

        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
