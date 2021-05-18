using DDD.Domain.Data;
using DDD.Domain.Model.Interface.Accounts;
using DDD.Domain.Models;
using DDD.Domain.ViewModels.Accounts;
using System.Linq;

namespace DDD.Domain.Model.Repository.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TestMakerContext _context;

        public AccountRepository(TestMakerContext context)
        {
            _context = context;
        }

        public User GetSelectedUser(AccountLoginViewModel loginUser)
        {
            return _context.Users.SingleOrDefault(o => o.LoginId == loginUser.LoginId);
        }
    }
}
