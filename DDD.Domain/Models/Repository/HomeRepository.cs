using System.Linq;
using DDD.Domain.Data;
using DDD.Domain.Model.Interface;
using DDD.Domain.Models.ViewModels;

namespace DDD.Domain.Model.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly TestMakerContext _context;

        public HomeRepository(TestMakerContext context)
        {
            _context = context;
        }

        public HomeViewModel GetAll()
        {
            return new HomeViewModel
            {
                Tests = _context.Tests.ToList(),
                Users = _context.Users.ToList()
            };
        }
    }
}
