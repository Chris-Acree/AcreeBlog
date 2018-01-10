using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetAuthorByAppUserIdQuery : IQuery<Author>
  {
    public string Id { get; set; }
  }
}
