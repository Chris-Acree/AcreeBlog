

namespace AcreeBlog.ViewModels
{
    public class BlogPostCategoryViewModel
    {
        public long BlogPostId { get; set; }
        public BlogPostViewModel BlogPost { get; set; }
        public long CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
