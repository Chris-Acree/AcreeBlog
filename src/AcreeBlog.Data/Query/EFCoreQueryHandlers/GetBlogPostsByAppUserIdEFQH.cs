﻿using AcreeBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetBlogPostsByAppUserIdEFQH : EFQHBase, IQueryHandlerAsync<GetBlogPostsByAppUserIdQuery, PaginatedList<BlogPost>>
  {
    public GetBlogPostsByAppUserIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<BlogPost>> HandleAsync(GetBlogPostsByAppUserIdQuery query)
    {
      var posts = (from a in _context.Authors
                   where a.ApplicationUserId == query.AppUserId
                   join bp in _context.BlogPosts on a.Id equals bp.AuthorId
                   orderby bp.ModifiedAt descending
                   select bp);

      var paginatedPosts = await PaginatedList<BlogPost>.CreateAsync(posts, query.CurrentPage, query.PageSize);

      return paginatedPosts;
    }
  }
}
