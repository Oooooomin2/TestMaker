using System;
using System.Linq.Expressions;

namespace DDD.Domain.Model.Interface
{
    public interface IRepository<T> where T : class
    {
        public void Create(T model);

        public void Update(T model);

        public void Delete(int id);

        public bool Exists(Expression<Func<T, bool>> expression);
    }
}
