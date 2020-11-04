﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using Prometheus;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Counter counter = Metrics
            .CreateCounter("my_custom_counter", "Metrics counter");

        public IActionResult Index()
        {
            counter.Inc();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            counter.Inc();
            
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
