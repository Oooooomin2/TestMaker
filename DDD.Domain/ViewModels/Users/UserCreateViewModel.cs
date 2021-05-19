using DDD.Domain.Models;
using DDD.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace DDD.Domain.ViewModels.Users
{
    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        [LoginIdUnique]
        public string LoginId { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }
    }
}
