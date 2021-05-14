using System.Collections.Generic;

namespace DDD.Domain.Models.ViewModels
{
    public class TestViewModel
    {
        public Test Tests { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<Choice> Choices { get; set; }
    }
}
