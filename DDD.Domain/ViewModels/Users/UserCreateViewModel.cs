using DDD.Domain.Models;
using DDD.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace DDD.Domain.ViewModels.Users
{
    public class UserCreateViewModel : User
    {
        [LoginIdUnique]
        public override string LoginId { get; set; }
    }
}
