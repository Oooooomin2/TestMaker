using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMaker.Data;
using TestMaker.Models.Interface;

namespace TestMaker.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TestMakerContext _context;

        public UserRepository(TestMakerContext context)
        {
            _context = context;
        }

        public void Create(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Users.Add(model);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var target = _context.Users.Find(id);
            _context.Users.Remove(target);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetContent(Expression<Func<User, bool>> expression)
        {
            return _context.Users.SingleOrDefault(expression);
        }

        public User FindUser(int? id)
        {
            return _context.Users.Find(id);
        }

        public bool LoginIdExists(User user)
        {
            return _context.Users.Where(o => o.LoginId == user.LoginId).Any();
        }

        public void Update(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            else
            {
                _context.Users.Update(model);
                _context.SaveChanges();
            }
        }

        public bool Exists(Expression<Func<User, bool>> expression)
        {
            return _context.Users.Any(expression);
        }
    }
}
