using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            else
            {
                _context.Tests.Add(viewModel.Tests);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var target = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(target);
            await _context.SaveChangesAsync();
        }

        public bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.TestId == id);
        }
        public TestViewModel GetContent(int? id)
        {
            return new TestViewModel
            {
                Tests = _context.Tests
                    .SingleOrDefault(m => m.TestId == id),
                Questions = _context.Questions
                    .Where(m => m.TestId == id)
                    .ToList(),
                Choices = _context.Choices
                    .Where(m => m.Question.TestId == id)
                    .ToList()
            };
        }

        public Test GetDeleteContent(int? id)
        {
            return _context.Tests.SingleOrDefault(m => m.TestId == id);
        }

        public UserTestViewModel GetAll(int? id)
        {
            return new UserTestViewModel
            {
                Tests = _context.Tests
                    .Where(m => m.UserId == id)
                    .ToList(),
                User = _context.Users
                    .SingleOrDefault(m => m.UserId == id)
            };
        }

        public IQueryable<Question> GetQuestion(int id)
        {
            return _context.Questions
                .Where(o => o.TestId == id)
                .Include(o => o.Choices);
        }

        public void Update(TestViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }
            else
            {
                _context.Tests.Update(viewModel.Tests);
                _context.SaveChanges();
            }
        }

    }
}
