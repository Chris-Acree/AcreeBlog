using AcreeBlog.Models.ViewModels.Home;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetBlogPostByIdViewModelQuery : IQuery<BlogPostViewModel>
  {
    public long Id { get; set; }
  }
}
