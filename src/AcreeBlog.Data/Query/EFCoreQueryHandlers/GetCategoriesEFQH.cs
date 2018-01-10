using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetCategoriesEFQH : EFQHBase, IQueryHandlerAsync<GetCategoriesQuery, IEnumerable<Category>>
  {
    public GetCategoriesEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> HandleAsync(GetCategoriesQuery query)
    {
      return await Task.FromResult(_context.Categories);
    }
  }
}
