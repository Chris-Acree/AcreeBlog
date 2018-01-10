
using System.Collections.Generic;

namespace AcreeBlog.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BlogPostCategoryViewModel> BlogPosts { get; set; }
    }
}
