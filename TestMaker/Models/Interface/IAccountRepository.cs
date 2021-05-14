using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface IAccountRepository
    {
        public User GetSelectedUser(Login loginUser);
    }
}
