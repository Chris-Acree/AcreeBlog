
using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.ViewModels
{
    public class AuthorViewModel
    {
        public long Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Blurb { get; set; }
        public string Biography { get; set; }
        public string ImageLink { get; set; }
        public virtual ICollection<BlogPostViewModel> BlogPosts { get; set; }
    }
}
