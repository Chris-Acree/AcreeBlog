using AcreeBlog.Models.ViewModels.Home;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetCategoryPostsByCategoryViewModelQuery : IQuery<CategoryPostsViewModel>
  {
    public long Id { get; set; }
  }
}
