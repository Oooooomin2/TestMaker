using System.Collections.Generic;

namespace TestMakerProject.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public IList<Choice> Choices { get; set; }
    }
}
