using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AcreeBlog.Data.RepositoryCommon
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    //https://stackoverflow.com/questions/46206997/asp-net-core-2-0-repository-pattern-with-a-generic-base-class
    //public class Repository<T> where T : class
    //public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly BlogDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(BlogDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        /*
        public IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            //return entities.AsEnumerable().SingleOrDefault(predicate);
            //cannot convert from 'system.linq.expressions.expression system.func t bool ' to 'system func<t, bool>'

            //return entities.AsEnumerable().SingleOrDefault(Expression.Lambda(predicate).Compile());
            
        }
        */

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Dispose()
        {
            //return false;
        }
    }
}
