using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface ITestRepository
    {
        public void Create(TestViewModel viewModel);

        public void Update(TestViewModel viewModel);

        public Task DeleteAsync(int id);

        public TestViewModel GetContent(int? id);

        public Test GetDeleteContent(int? id);

        public UserTestViewModel GetAll(int? id);

        public IQueryable<Question> GetQuestion(int id);

        public bool TestExists(int id);
    }
}
