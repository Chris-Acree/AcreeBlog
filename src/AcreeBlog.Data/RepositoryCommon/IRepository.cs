using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AcreeBlog.Data.RepositoryCommon
{
    //public interface IRepository<T>
    public interface IRepository<T> where T : class
    {
        T GetById(long id);
        IEnumerable<T> GetAll();
        //IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
