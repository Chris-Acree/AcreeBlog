using AcreeBlog.Models.ViewModels.Admin;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetManagePostsViewModelQuery : IQuery<ManagePostsViewModel>
  {
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
  }
}
