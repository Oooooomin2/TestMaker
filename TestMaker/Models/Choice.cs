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
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public bool IsAnswer { get; set; }
        [NotMapped]
        public bool IsUsersAnswerCheck { get; set; }
        [NotMapped]
        public int IsUsersAnswerRadio { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}