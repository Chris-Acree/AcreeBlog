
namespace AcreeBlog.ViewModels
{
    public class BlogPostTopicViewModel
    {
        public long BlogPostId { get; set; }
        public BlogPostViewModel BlogPost { get; set; }
        public long TopicId { get; set; }
        public TopicViewModel Topic { get; set; }
    }
}
