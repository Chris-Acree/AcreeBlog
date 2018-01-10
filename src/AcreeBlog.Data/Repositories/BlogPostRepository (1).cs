using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.RepositoryCommon;
using Microsoft.EntityFrameworkCore;

namespace AcreeBlog.Data.Repositories
{
    public class BlogPostRepository : Repository<Category>
    {
        private readonly BlogDbContext _dbContext;
        public int _currentPage { get; set; }
        public int _pageSize { get; set; }

        public BlogPostRepository(BlogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<BlogPost>> GetAllPostsPaginatedAsync(int currentPage, int pageSize)
        {
            var posts = await PaginatedList<BlogPost>.CreateAsync(_dbContext.BlogPosts
                  .OrderByDescending(bp => bp.ModifiedAt)
                  .Include(a => a.Author),
                  currentPage,
                  pageSize);
                  /*,
                  query.CurrentPage,
                  query.PageSize)*/

            return posts;
        }
    }

}
