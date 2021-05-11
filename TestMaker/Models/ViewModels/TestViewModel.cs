using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Data;

namespace TestMaker.Models.ViewModels
{
    public class TestViewModel
    {
        public Test Tests { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<Choice> Choices { get; set; }

        public TestViewModel ShowTestDetailsEditScoreInfo(int? id, TestMakerContext _context)
        {
            return new TestViewModel
            {
                Tests = _context.Tests
                    .FirstOrDefault(m => m.TestId == id),
                Questions = _context.Questions
                    .Where(m => m.TestId == id)
                    .ToList(),
                Choices = _context.Choices
                    .Where(m => m.Question.TestId == id)
                    .ToList()
            };
        }
    }
}
