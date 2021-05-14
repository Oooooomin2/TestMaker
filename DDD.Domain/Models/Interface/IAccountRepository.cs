using DDD.Domain.Models;

namespace DDD.Domain.Model.Interface
{
    public interface IAccountRepository
    {
        public User GetSelectedUser(Login loginUser);
    }
}
