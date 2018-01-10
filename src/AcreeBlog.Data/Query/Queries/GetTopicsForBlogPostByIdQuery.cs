using System.Collections.Generic;
using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetTopicsForBlogPostByIdQuery : IQuery<IEnumerable<Topic>>
  {
    public long Id { get; set; }
  }
}
