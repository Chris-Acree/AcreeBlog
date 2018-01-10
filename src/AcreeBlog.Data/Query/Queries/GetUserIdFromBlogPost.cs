using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetUserIdFromBlogPost : IQuery<string>
  {
    public BlogPost blogpost { get; set; }
  }
}
