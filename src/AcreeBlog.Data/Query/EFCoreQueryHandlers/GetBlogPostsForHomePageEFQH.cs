﻿using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetBlogPostsForHomePageEFQH : EFQHBase, IQueryHandlerAsync<GetBlogPostsForHomePageQuery, PaginatedList<BlogPost>>
  {
    public GetBlogPostsForHomePageEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<BlogPost>> HandleAsync(GetBlogPostsForHomePageQuery query)
    {
      var posts = _context.BlogPosts
        .Where(bp => bp.PublishOn < DateTime.Now && bp.Public == true)
        .OrderByDescending(bp => bp.PublishOn)
        .Include(bp => bp.Author)
        .Include(bp => bp.Comments);

      return await PaginatedList<BlogPost>.CreateAsync(posts, query.CurrentPage, query.PageSize);
    }
  }
}
