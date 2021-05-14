using System.Collections.Generic;

namespace DDD.Domain.Models.ViewModels
{
    public class UserTestViewModel
    {
        public IList<Test> Tests { get; set; }
        public User User { get; set; }
    }
}
