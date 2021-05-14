using DDD.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Interface
{
    public interface ITestRepository : IRepository<Test>
    {
        public Test GetDeleteContent(int? id);

        public IEnumerable<Test> GetAll(int? id);

        public IQueryable<Question> GetQuestion(int id);
    }
}
