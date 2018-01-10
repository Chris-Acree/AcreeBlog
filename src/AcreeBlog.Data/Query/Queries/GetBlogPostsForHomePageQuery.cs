using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetBlogPostsForHomePageQuery : IQuery<PaginatedList<BlogPost>>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }
}
