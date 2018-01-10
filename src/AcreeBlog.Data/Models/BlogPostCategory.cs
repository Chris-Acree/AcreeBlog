namespace AcreeBlog.Data.Models
{
    public class BlogPostCategory
  {
    public long BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
  }
}
