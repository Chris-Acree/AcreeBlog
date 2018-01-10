using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetUserIdFromBlogPostEFQH : EFQHBase, IQueryHandlerAsync<GetUserIdFromBlogPost, string>
  {
    public GetUserIdFromBlogPostEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<string> HandleAsync(GetUserIdFromBlogPost query)
    {
      var appUserId = await (from bp in _context.BlogPosts
                            where bp == query.blogpost
                            select bp.Author.ApplicationUserId).FirstOrDefaultAsync();

      return appUserId;
    }
  }
}
