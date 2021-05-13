using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;

namespace TestMaker.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Test> Tests { get; set; }
        public List<User> Users { get; set; }
    }
}
