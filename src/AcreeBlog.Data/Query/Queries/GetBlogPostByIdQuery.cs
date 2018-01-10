using AcreeBlog.Data.Models;

namespace AcreeBlog.Data.Query.Queries
{
    public class GetBlogPostByIdQuery : IQuery<BlogPost>
    {
        public long Id { get; set; }
    }
}
