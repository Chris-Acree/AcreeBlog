using System;
using System.ComponentModel.DataAnnotations;

namespace AcreeBlog.Data.Models
{
    public class Comment
  {
    public Comment()
    {
      Added = DateTime.Now;
    }

    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
    public string WebSite { get; set; }
    [Required]
    public long BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }

    [Required]
    public string Text { get; set; }

    public DateTime Added { get; set; }
  }
}
