using AcreeBlog.Data.Query.Queries;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetIsUserAnAuthorEFQH : EFQHBase, IQueryHandlerAsync<GetIsUserAnAuthorQuery, bool>
  {
    public GetIsUserAnAuthorEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<bool> HandleAsync(GetIsUserAnAuthorQuery query)
    {
      var author = await _context.Authors.AsNoTracking().Where(a => a.ApplicationUserId == query.Id).FirstOrDefaultAsync();

      return author != null;
    }
  }
}
