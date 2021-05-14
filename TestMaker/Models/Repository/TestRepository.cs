using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models.Interface;
using TestMaker.Models.ViewModels;

namespace TestMaker.Models.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly TestMakerContext _context;

        public TestRepository(TestMakerContext context)
        {
            _context = context;
        }

        public void Create(Test model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Tests.Add(model);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var target = _context.Tests.Find(id);
            _context.Tests.Remove(target);
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<Test, bool>> expression)
        {
            return _context.Tests.Any(expression);
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

        public void Update(Test model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Tests.Update(model);
                _context.SaveChanges();
            }
        }

    }
}
