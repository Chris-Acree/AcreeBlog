using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Models.ViewModels.Admin;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetManageTopicsViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetManageTopicsViewModelQuery, ManageTopicsViewModel>
  {
    public GetManageTopicsViewModelEFQH(BlogDbContext context) : base(context) { }

    public async Task<ManageTopicsViewModel> HandleAsync(GetManageTopicsViewModelQuery query)
    {
      ManageTopicsViewModel mfvm = new ManageTopicsViewModel()
      {
        Topics = await _context.Topics.AsNoTracking().ToListAsync()
      };
      return mfvm;
    }
  }
}
