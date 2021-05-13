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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TestMakerContext _context;

        public UserRepository(TestMakerContext context) 
            : base(context)
        {
            _context = context;
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
    }
}
