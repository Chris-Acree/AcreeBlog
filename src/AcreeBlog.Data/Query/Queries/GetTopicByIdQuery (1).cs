using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetTopicByIdQuery : IQuery<Topic>
  {
    public long Id { get; set; }
  }
}
