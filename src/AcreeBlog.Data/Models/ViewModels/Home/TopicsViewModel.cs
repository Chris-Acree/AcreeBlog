using System.Collections.Generic;

namespace AcreeBlog.Models.ViewModels.Home
{
    public class TopicsViewModel
  {
    public IEnumerable<TopicWithBlogCountViewModel> Topics { get; set; }
  }
  public class TopicWithBlogCountViewModel
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int BlogCount { get; set; }
  }
}
