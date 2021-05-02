using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models;

namespace TestMaker.Validations
{
    public class LoginIdUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var context = (TestMakerContext)validationContext.GetService(typeof(TestMakerContext));
            var entity = context.Users.SingleOrDefault(e => e.LoginId == value.ToString());
            if (entity != null)
            {
                return new ValidationResult($"This LoginId(Email address) \"{value}\" is already in use.");
            }
            return ValidationResult.Success;
        }
    }
}
