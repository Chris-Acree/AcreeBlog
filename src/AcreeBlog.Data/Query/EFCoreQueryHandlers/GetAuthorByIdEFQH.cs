using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetAuthorByIdEFQH : EFQHBase, IQueryHandlerAsync<GetAuthorByIdQuery, Author>
  {
    public GetAuthorByIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<Author> HandleAsync(GetAuthorByIdQuery query)
    {
      return await _context.Authors.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
    }
  }
}
