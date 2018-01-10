namespace AcreeBlog.Data.Models
{
    public class BlogPostTopic
  {
    public long BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
    public long TopicId { get; set; }
    public Topic Topic { get; set; }
  }
}
