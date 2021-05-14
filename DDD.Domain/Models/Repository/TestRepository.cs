using DDD.Domain.Data;
using DDD.Domain.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DDD.Domain.Models.Repository
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        private readonly TestMakerContext _context;

        public TestRepository(TestMakerContext context)
            : base(context)
        {
            _context = context;
        }
        public Test GetContent(Expression<Func<Test, bool>> expression)
        {
            return _context.Tests
                .Include(o => o.Questions)
                .ThenInclude(o => o.Choices)
                .SingleOrDefault(expression);
        }
        public Test GetDeleteContent(int? id)
        {
            return _context.Tests.SingleOrDefault(m => m.TestId == id);
        }

        public IEnumerable<Test> GetAll(int? id)
        {
            return _context.Tests
                .Where(m => m.UserId == id)
                .Include(o => o.User);
        }

        public IQueryable<Question> GetQuestion(int id)
        {
            return _context.Questions
                .Where(o => o.TestId == id)
                .Include(o => o.Choices);
        }
    }
}
