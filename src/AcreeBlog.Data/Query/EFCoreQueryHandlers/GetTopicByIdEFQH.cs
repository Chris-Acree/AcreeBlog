using AcreeBlog.Data.RepositoryCommon;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.Models;
using System.Threading.Tasks;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetTopicByIdEFQH : EFQHBase, IQueryHandlerAsync<GetTopicByIdQuery, Topic>
  {
    public GetTopicByIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public async Task<Topic> HandleAsync(GetTopicByIdQuery query)
    {
      var f = await _context.Topics.FindAsync(query.Id);

      if (f != null)
      {
        return f;
      }
      else
      {
        return null;
      }
    }
  }
}
