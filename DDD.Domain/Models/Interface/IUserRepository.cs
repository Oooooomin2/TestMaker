using DDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDD.Domain.Model.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public User FindUser(int? id);

        public bool LoginIdExists(User user);

        public User GetContent(Expression<Func<User, bool>> expression);

        public IEnumerable<User> GetAll();
    }
}
