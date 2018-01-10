using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Models.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetAllPostsByDateViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetAllPostsByDateViewModelQuery, AllPostsViewModel>
  {
    public GetAllPostsByDateViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<AllPostsViewModel> HandleAsync(GetAllPostsByDateViewModelQuery query)
    {
      return new AllPostsViewModel()
      {
        PostsByDate = await _context.BlogPosts
                                      .AsNoTracking()
                                      .Where(bp => bp.Public == true && bp.PublishOn < DateTime.Now)
                                      .Include(bp => bp.Author)
                                      .OrderByDescending(bp => bp.PublishOn)
                                      .ThenByDescending(bp => bp.ModifiedAt)
                                      .ToListAsync(),
        SortBy = 2,
        Categories = null
      };
    }
  }
}
