using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetBlogPostByIdEFQH : EFQHBase, IQueryHandlerAsync<GetBlogPostByIdQuery, BlogPost>
    {
        public GetBlogPostByIdEFQH(BlogDbContext context) : base(context)
        {
        }

        public async Task<BlogPost> HandleAsync(GetBlogPostByIdQuery query)
        {
            return await _context.BlogPosts.Where(bp => bp.Id == query.Id).Include(bp => bp.Author).FirstOrDefaultAsync();
        }
    }
}
