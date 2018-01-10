using System;
using System.Collections.Generic;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetRecentBlogPostsEFQH : EFQHBase, IQueryHandlerAsync<GetRecentBlogPostsQuery, IList<BlogPost>>
  {
    public GetRecentBlogPostsEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<IList<BlogPost>> HandleAsync(GetRecentBlogPostsQuery query)
    {
      var recentPosts = await _context.BlogPosts
                                .AsNoTracking()
                                .Where(bp => bp.Public == true && bp.PublishOn <= DateTime.Now)
                                .OrderByDescending(bp => bp.ModifiedAt)
                                .ThenByDescending(bp => bp.PublishOn)
                                .Take(query.NumberOfPostsToGet)
                                .ToListAsync();

      return recentPosts;
    }
  }
}
