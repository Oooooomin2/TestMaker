using DDD.Domain.Data;
using DDD.Domain.ViewModels.Accounts;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.Domain.Validations
{
    public class LoginIdExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var context = (TestMakerContext)validationContext.GetService(typeof(TestMakerContext));
            var entity = context.Users.Any(o => o.LoginId == value.ToString());
            if (!entity)
            {
                return new ValidationResult($"This LoginId(Email address) \"{value}\" is unregistered");
            }
            return ValidationResult.Success;
        }
    }
}
