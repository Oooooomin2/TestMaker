using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;

namespace TestMaker.Models.ViewModels
{
    public class UserTestViewModel
    {
        public IList<Test> Tests { get; set; }
        public User User { get; set; }
    }
}
