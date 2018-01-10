using AcreeBlog.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcreeBlog.Data.Query.Queries;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Query.EFCoreQueryHandlers
{
    public class GetAuthorsDictionaryKeyAppUserIdEFQH : EFQHBase, IQueryHandlerAsync<GetAuthorsDictionaryKeyAppUserIdQuery, Dictionary<string, Author>>
  {
    public GetAuthorsDictionaryKeyAppUserIdEFQH(BlogDbContext context) : base(context)
    {
    }

    public Task<Dictionary<string, Author>> HandleAsync(GetAuthorsDictionaryKeyAppUserIdQuery query)
    {
      var authorsDictionary = new Dictionary<string, Author>();

      foreach(var author in _context.Authors)
      {
        if (author.ApplicationUserId != null)
        {
          authorsDictionary[author.ApplicationUserId] = author; 
        }
      }

      return Task.FromResult(authorsDictionary);
    }
  }
}
