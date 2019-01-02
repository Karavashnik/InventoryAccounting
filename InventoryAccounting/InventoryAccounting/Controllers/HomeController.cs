﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryAccounting.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Controllers
{
    public class HomeController : Controller
    {
        private InventoryAccountingContext db;

        public HomeController(InventoryAccountingContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Tmc.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}