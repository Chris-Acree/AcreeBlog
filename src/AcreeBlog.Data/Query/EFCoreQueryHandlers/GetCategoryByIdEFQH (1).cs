using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetCategoryByIdEFQH : EFQHBase, IQueryHandlerAsync<GetCategoryByIdQuery, Category>
  {
    public GetCategoryByIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<Category> HandleAsync(GetCategoryByIdQuery query)
    {
      var c = await _context.Categories.FindAsync(query.Id);

      return c;
    }
  }
}
