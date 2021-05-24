using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMakerProject.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string UpdatedTime { get; set; }
        public string UserName { get; set; }
    }
}
