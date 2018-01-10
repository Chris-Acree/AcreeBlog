using System.Collections.Generic;

namespace AcreeBlog.Data.Command.Commands
{
    public class SetBlogPostCategoriesCommand
  {
    public long BlogPostId { get; set; }
    public IEnumerable<long> CategoryIds { get; set; }
  }
}
