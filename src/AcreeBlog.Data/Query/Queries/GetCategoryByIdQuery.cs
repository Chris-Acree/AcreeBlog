using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetCategoryByIdQuery : IQuery<Category>
  {
    public long Id { get; set; }
  }
}
