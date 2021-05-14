using DDD.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Data;

namespace TestMaker.Models.Repository
{
    public class GenericRepository<T> : IRepository<T> 
        where T : class
    {
        private readonly TestMakerContext _context;

        public GenericRepository(TestMakerContext context)
        {
            _context = context;
        }

        public void Create(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Set<T>().Add(model);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id));
            _context.SaveChanges();
        }

        public T GetContent(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().SingleOrDefault(expression);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Set<T>().Update(model);
                _context.SaveChanges();
            }
        }
    }
}
