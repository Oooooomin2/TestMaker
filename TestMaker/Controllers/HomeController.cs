using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.ViewModels;

namespace TestMaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestMakerContext _context;

        public HomeController(TestMakerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Action"] = "Index";
            ViewData["Controller"] = "Home";
            return View(new HomeIndexViewModel().ShowHomeInfo(_context));
        }

        public IActionResult Description()
        {
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
