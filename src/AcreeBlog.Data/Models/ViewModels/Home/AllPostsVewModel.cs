using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Models.ViewModels.Home
{
    public class AllPostsViewModel
    {
        public int SortBy { get; set; }
        public IEnumerable<BlogPost> PostsByDate { get; set; }
        public IEnumerable<PostsByCategoryViewModel> Categories { get; set; }
    }

    public class PostsByCategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<BlogPost> Posts { get; set; }
    }
}
