using System.Collections.Generic;

namespace TestMakerProject.Controllers.Resources
{
    public class UserResource
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
