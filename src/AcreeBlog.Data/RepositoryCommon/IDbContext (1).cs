using System;
using Microsoft.EntityFrameworkCore;

namespace AcreeBlog.Data.RepositoryCommon
{
    public interface IDbContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        //DbEntityEntry Entry(object entry);
        int SaveChanges();
    }
}
