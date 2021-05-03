using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Validations;

namespace TestMaker.Models
{
    public class NewUserRegist
    {
        [Required]
        [EmailAddress]
        [LoginIdUnique]
        public string LoginId { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
