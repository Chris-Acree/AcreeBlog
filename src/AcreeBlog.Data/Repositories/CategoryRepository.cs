using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        private readonly BlogDbContext _dbContext;

        public CategoryRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
