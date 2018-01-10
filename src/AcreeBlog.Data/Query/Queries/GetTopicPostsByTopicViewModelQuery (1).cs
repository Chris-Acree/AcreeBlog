using AcreeBlog.Models.ViewModels.Home;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetTopicPostsByTopicViewModelQuery : IQuery<TopicPostsViewModel>
  {
    public long Id { get; set; }
  }
}
