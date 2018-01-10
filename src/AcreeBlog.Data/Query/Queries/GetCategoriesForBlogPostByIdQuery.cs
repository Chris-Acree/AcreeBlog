using AcreeBlog.Data.Models;
using System.Collections.Generic;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetCategoriesForBlogPostByIdQuery : IQuery<IEnumerable<Category>>
  {
    public long Id { get; set; }
  }
}
