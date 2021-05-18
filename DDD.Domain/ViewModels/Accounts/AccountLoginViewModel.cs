using DDD.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace DDD.Domain.ViewModels.Accounts
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        [LoginIdExists]
        public string LoginId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
