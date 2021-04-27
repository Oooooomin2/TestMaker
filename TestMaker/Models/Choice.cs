using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaker.Models
{
    public class Choice
    {
        [Key]
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsAnswer { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
