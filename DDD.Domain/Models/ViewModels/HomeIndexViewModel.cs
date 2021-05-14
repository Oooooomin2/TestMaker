using System.Collections.Generic;

namespace DDD.Domain.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Test> Tests { get; set; }
        public List<User> Users { get; set; }
    }
}
