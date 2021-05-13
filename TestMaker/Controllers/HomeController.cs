using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models;
using TestMaker.Models.Interface;
using TestMaker.Models.ViewModels;

namespace TestMaker.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Action"] = "Index";
            ViewData["Controller"] = "Home";
            return View(_homeRepository.GetAll());
        }

        public IActionResult Description()
        {
            ViewData["Title"] = "What's the Test Maker?";
            ViewData["Action"] = "Description";
            ViewData["Controller"] = "Home";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy";
            ViewData["Action"] = "Privacy";
            ViewData["Controller"] = "Home";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
