using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.ViewModels.Home
{
    public class TopicPostsViewModel
  {
    public Topic Topic { get; set; }
    public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }
  }
}
