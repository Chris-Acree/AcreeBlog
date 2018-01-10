using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Models.ViewModels.Admin;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetManagePostsViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetManagePostsViewModelQuery, ManagePostsViewModel>
  {
    public GetManagePostsViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<ManagePostsViewModel> HandleAsync(GetManagePostsViewModelQuery query)
    {
      var mpvm = new ManagePostsViewModel()
      {
        posts = await PaginatedList<BlogPost>.CreateAsync(_context.BlogPosts.AsNoTracking()
          .OrderByDescending(bp => bp.ModifiedAt)
          .Include(a => a.Author), 
          query.CurrentPage,
          query.PageSize)
      };

      return mpvm;
    }
  }
}
