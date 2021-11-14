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

    }
    }
