using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        public void Create(T model);

        public void Update(T model);

        public void Delete(int id);

        public bool Exists(Expression<Func<T, bool>> expression);
    }
}
