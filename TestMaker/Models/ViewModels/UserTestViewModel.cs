using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models.ViewModels
{
    public class UserTestViewModel
    {
        public ICollection<Test> Tests { get; set; }
        public User User { get; set; }
    }
}
