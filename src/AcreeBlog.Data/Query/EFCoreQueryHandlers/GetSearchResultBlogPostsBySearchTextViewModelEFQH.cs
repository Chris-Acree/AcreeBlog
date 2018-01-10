using System;
using System.Collections.Generic;
using AcreeBlog.Models.ViewModels.Home;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetSearchResultBlogPostsBySearchTextViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetSearchResultBlogPostsBySearchTextViewModelQuery, IList<SearchResultBlogPostViewModel>>
  {
    public GetSearchResultBlogPostsBySearchTextViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<IList<SearchResultBlogPostViewModel>> HandleAsync(GetSearchResultBlogPostsBySearchTextViewModelQuery query)
    {
      var posts = await _context.BlogPosts
                          .AsNoTracking()
                          .FromSql("SELECT * from [dbo].[BlogPost] WHERE Contains((Content, Description, Title), {0})", query.SearchText)
                          .Where(bp => bp.Public == true && bp.PublishOn < DateTime.Now)
                          .OrderByDescending(bp => bp.PublishOn)
                          .ThenByDescending(bp => bp.ModifiedAt)
                          .Include(bp => bp.Author)
                          .Include(bp => bp.Comments)
                          .Include(bp => bp.BlogPostCategory)
                            .ThenInclude(bpc => bpc.Category)
                          .ToListAsync();

      var bps = new List<SearchResultBlogPostViewModel>();

      foreach (var post in posts)
      {

        var categories = post.BlogPostCategory.Select(c => c.Category).ToList();

        var vsrbpvm = new SearchResultBlogPostViewModel()
        {
          BlogPost = post,
          Categories = categories
        };

        bps.Add(vsrbpvm);

      }

      return bps;
    }
  }
}
