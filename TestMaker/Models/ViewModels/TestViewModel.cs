using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models.ViewModels
{
    public class TestViewModel
    {
        public Test Tests { get; set; }
        public IList<Choice> Choices { get; set; }
    }
}
