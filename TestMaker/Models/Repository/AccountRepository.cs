using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models.Interface;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Repository
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
