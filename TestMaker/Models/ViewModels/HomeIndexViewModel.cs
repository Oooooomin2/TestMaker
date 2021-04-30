using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public ICollection<Test> Tests { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
