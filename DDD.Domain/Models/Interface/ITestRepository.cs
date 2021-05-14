using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDD.Domain.Model.Interface
{
    public interface ITestRepository : IRepository<Test>
    {
        public Test GetDeleteContent(int? id);

        public IEnumerable<Test> GetAll(int? id);

        public Test GetContent(Expression<Func<Test, bool>> expression);

        public IQueryable<Question> GetQuestion(int id);
    }
}
