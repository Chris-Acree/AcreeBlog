using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetAuthorByAppUserIdEFQH : EFQHBase, IQueryHandlerAsync<GetAuthorByAppUserIdQuery, Author>
  {
    public GetAuthorByAppUserIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<Author> HandleAsync(GetAuthorByAppUserIdQuery query)
    {
      // This needs to be returned as tracking or another query needs to be made
      return await _context.Authors.Where(a => a.ApplicationUserId == query.Id).FirstOrDefaultAsync();
    }
  }
}
