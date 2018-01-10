using AcreeBlog.Data.Models;


namespace AcreeBlog.Data.Query.Queries
{
    public class GetBlogPostsByAppUserIdQuery : IQuery<PaginatedList<BlogPost>>
  {
    public string AppUserId { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }
}
