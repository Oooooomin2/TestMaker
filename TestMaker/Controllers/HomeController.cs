using DDD.Domain.Model.Interface;
using DDD.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
