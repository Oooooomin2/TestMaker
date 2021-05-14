using System.ComponentModel.DataAnnotations;

namespace DDD.Domain.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string LoginId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
