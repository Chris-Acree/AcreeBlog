using System.Collections.Generic;

namespace AcreeBlog.Data.Command.Commands
{
    public class SetBlogPostCategoriesByStringArrayCommand
  {
    public IList<string> Categories { get; set; }
    public long BlogPostId { get; set; }
  }
}
