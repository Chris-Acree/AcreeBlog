using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Repositories
{
    public class AuthorRepository : Repository<Category>
    {
        private readonly BlogDbContext _dbContext;

        public AuthorRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
