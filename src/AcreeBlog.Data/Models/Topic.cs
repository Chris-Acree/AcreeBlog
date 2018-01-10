using System.Collections.Generic;
using AcreeBlog.Data.RepositoryCommon;

namespace AcreeBlog.Data.Models
{
  public class Topic : BaseEntity
  {
    //public override long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool ShowOnHomePage { get; set; }
    public ICollection<BlogPostTopic> BlogPosts{ get; set; }
  }
}
