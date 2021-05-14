using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models.Interface;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly TestMakerContext _context;

        public HomeRepository(TestMakerContext context)
        {
            _context = context;
        }

        public HomeIndexViewModel GetAll()
        {
            return new HomeIndexViewModel
            {
                Tests = _context.Tests.ToList(),
                Users = _context.Users.ToList()
            };
        }
    }
}
