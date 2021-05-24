using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerProject.Models;
using TestMakerProject.Models.ViewModels;
using TestMakerProject.Persistence;

namespace TestMakerProject.Controllers
{
    [Route("/api/home")]
    public class HomeController : Controller
    {
        private readonly TestMakerContext _context;
        private readonly IMapper _mapper;

        public HomeController(TestMakerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HomeIndexViewModel>> GetAllTest()
        {
            var tests = await _context.Tests
                .Include(m => m.User)
                .ToListAsync();
            return _mapper.Map<List<Test>, List<HomeIndexViewModel>>(tests);
        }
    }
}
