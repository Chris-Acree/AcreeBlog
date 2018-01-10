using System.Collections.Generic;

namespace AcreeBlog.Data.Command.Commands
{
    public class SetBlogPostTopicsCommand
  {
    public long BlogPostId { get; set; }
    public IEnumerable<long> TopicIds { get; set; }
  }
}
