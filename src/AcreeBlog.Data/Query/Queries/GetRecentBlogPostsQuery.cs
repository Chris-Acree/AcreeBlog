using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetRecentBlogPostsQuery : IQuery<IList<BlogPost>>
  {
    public int NumberOfPostsToGet { get; set; }
  }

}
