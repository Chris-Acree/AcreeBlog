using System.Collections.Generic;
using AcreeBlog.Data.Models;
using AcreeBlog.Data.Query.Queries;
using System.Threading.Tasks;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetTopicsEFQH : EFQHBase, IQueryHandlerAsync<GetTopicsQuery, IEnumerable<Topic>>
  {
    public GetTopicsEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Topic>> HandleAsync(GetTopicsQuery query)
    {
      return await Task.FromResult(_context.Topics);
    }
  }
}
