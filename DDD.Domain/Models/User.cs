using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string LoginId { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }
        [Display(Name = "Self introduction")]
        public string SelfIntroduction { get; set; }
        public byte[] Icon { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
