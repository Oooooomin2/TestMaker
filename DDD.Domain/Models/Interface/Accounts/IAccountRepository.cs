using DDD.Domain.Models;
using DDD.Domain.ViewModels.Accounts;

namespace DDD.Domain.Model.Interface.Accounts
{
    public interface IAccountRepository
    {
        public User GetSelectedUser(AccountLoginViewModel loginUser);
    }
}
