using System.Collections.Generic;
using DDD.Domain.Models;

namespace DDD.Domain.ViewModels.Home
{
    public class HomeViewModel
    {
        public IReadOnlyList<Test> Tests { get; set; }
        public IReadOnlyList<User> Users { get; set; }
    }
}
