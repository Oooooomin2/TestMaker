using DDD.Domain.Validations;
using System.Collections.Generic;

namespace DDD.Domain.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        [Choices]
        public IList<Choice> Choices { get; set; }
    }
}
