using AcreeBlog.Data.Query.Queries;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Models.ViewModels.Admin;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetManageCategoriesViewModelEFQH : EFQHBase, IQueryHandlerAsync<GetManageCategoriesViewModelQuery, ManageCategoriesViewModel>
  {
    public GetManageCategoriesViewModelEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<ManageCategoriesViewModel> HandleAsync(GetManageCategoriesViewModelQuery query)
    {
      ManageCategoriesViewModel amcvm = new ManageCategoriesViewModel()
      {
        Categories = await _context.Categories.AsNoTracking().ToListAsync()
      };
      return amcvm;
    }
  }
}
