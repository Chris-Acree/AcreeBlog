using AcreeBlog.Models.ViewModels.Home;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetBlogPostBySlugViewModelQuery : IQuery<BlogPostViewModel>
  {
    public string Slug { get; set; }
  }
}
