using System.Collections.Generic;

namespace DDD.Domain.Models.ViewModels
{
    public class HomeViewModel
    {
        public IReadOnlyList<Test> Tests { get; set; }
        public IReadOnlyList<User> Users { get; set; }
    }
}
