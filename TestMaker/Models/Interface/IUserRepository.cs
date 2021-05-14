using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface IUserRepository
    {
        public Task CreateAsync(User model);

        public Task UpdateAsync(User model);

        public Task DeleteAsync(int id);

        public Task<User> GetContent(int? id);

        public User FindUser(int? id);

        public bool LoginIdExists(User user);

        public Task<List<User>> GetAll();

        public bool UserExists(int id);
    }
}
