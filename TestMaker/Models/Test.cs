using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; }

        public ICollection<Choice> Choices { get; set; }
    }
}
