using System;

namespace AcreeBlog.Data.RepositoryCommon
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
