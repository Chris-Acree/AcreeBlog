using System;
using System.Collections.Generic;
using AcreeBlog.Data.Models;
using AcreeBlog.Models.ViewModels.Home;

namespace AcreeBlog.ViewModels.Home
{
    public class AllPostsViewModel
    {
        public int SortBy { get; set; }
        public IEnumerable<BlogPost> PostsByDate { get; set; }
        public IEnumerable<PostsByCategoryViewModel> Categories { get; set; }

        public static implicit operator AllPostsViewModel(AcreeBlog.Models.ViewModels.Home.AllPostsViewModel v)
        {
            throw new NotImplementedException();
        }
    }
    public class PostsByCategoryViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<BlogPost> Posts { get; set; }
    }
}
