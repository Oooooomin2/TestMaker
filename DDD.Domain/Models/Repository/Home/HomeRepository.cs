using System.Linq;
using DDD.Domain.Data;
using DDD.Domain.Model.Interface.Home;
using DDD.Domain.ViewModels.Home;

namespace DDD.Domain.Model.Repository.Home
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
