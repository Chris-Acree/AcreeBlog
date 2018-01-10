
using Microsoft.EntityFrameworkCore;

namespace AcreeBlog.Data.RepositoryCommon
{
    class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
