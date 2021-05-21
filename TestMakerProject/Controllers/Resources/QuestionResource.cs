using System.Collections.Generic;

namespace TestMakerProject.Controllers.Resources
{
    public class QuestionResource
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public int TestId { get; set; }
        public IList<ChoiceResource> Choices { get; set; }
    }
}
