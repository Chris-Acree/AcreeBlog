using System.Collections.Generic;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Models
{
    public class Category : BaseEntity
    {
    //public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<BlogPostCategory> BlogPosts { get; set; }
  }
}
