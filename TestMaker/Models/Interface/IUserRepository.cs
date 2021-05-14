using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface IUserRepository
    {
        public void Create(User model);

        public void Update(User model);

        public void Delete(int id);

        public User GetContent(int? id);

        public User FindUser(int? id);

        public bool LoginIdExists(User user);

        public List<User> GetAll();

        public bool UserExists(int id);
    }
}
