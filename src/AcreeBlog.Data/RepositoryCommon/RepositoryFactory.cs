using System;
using AcreeBlog.Data.Repositories;

namespace AcreeBlog.Data.RepositoryCommon
{
    class RepositoryFactory
    {
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }

        private IServiceProvider _provider;

    }
}
