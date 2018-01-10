using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.ViewModels.Home
{
    public class HomeViewModel
  {
    public IEnumerable<Category> Categories { get; set; }
    public PaginatedList<BlogPost> RecentPosts { get; set; }
  }
}
