using DDD.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public User FindUser(int? id);

        public bool LoginIdExists(User user);

        public IEnumerable<User> GetAll();
    }
}
