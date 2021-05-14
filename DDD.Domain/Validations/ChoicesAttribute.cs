using DDD.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.Domain.Validations
{
    public class ChoicesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            var checkList = (IList<Choice>)value;
            if (!checkList.Any(o => o.IsAnswer == true))
            {
                return new ValidationResult("Choose at least one answer.");
            }
            return ValidationResult.Success;
        }
    }
}
