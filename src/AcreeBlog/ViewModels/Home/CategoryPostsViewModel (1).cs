using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.ViewModels.Home
{
    public class CategoryPostsViewModel
  {
    public Category Category { get; set; }
    public IEnumerable<BlogPost> Posts { get; set; }
  }
}
