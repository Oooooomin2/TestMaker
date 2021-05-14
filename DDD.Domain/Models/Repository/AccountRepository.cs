using DDD.Domain.Data;
using DDD.Domain.Model.Interface;
using DDD.Domain.Models;
using System.Linq;

namespace DDD.Domain.Model.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TestMakerContext _context;

        public AccountRepository(TestMakerContext context)
        {
            _context = context;
        }

        public User GetSelectedUser(Login loginUser)
        {
            return _context.Users.SingleOrDefault(o => o.LoginId == loginUser.LoginId);
        }
    }
}
