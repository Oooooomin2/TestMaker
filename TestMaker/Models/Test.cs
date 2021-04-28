using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
