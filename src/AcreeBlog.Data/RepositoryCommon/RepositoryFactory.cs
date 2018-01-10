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

        public TopicRepository GetTopicRepository()
        {
            return _provider.GetService<TopicRepository>();
        }

        public CategoryRepository GetCategoryRepository()
        {
            return _provider.GetService<CategoryRepository>();
        }
    }
}
