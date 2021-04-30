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
        private readonly ILogger<HomeController> _logger;
        private readonly TestMakerContext _context;

        public HomeController(ILogger<HomeController> logger, TestMakerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var index = new HomeIndexViewModel
            {
                Tests = _context.Tests.ToList(),
                Users = _context.Users.ToList()
            };
            return View(index);
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
