﻿using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class OrderItemsController : Controller
    {



        public ActionResult AddToCart(Product product)
        {

            return View();
        }





        // GET: OrderItemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderItemController/Create
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

        // GET: OrderItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderItemController/Edit/5
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

        // GET: OrderItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderItemController/Delete/5
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
